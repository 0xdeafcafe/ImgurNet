using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ImgurNet.Authentication;
using ImgurNet.Exceptions;
using ImgurNet.Models;
using ImgurNet.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ImgurNet.ApiEndpoints
{
	public class GalleryEndpoint : BaseEndpoint
	{
		#region Endpoints

		internal const string GalleryRemovalUrl =			"gallery/{0}";
		internal const string GallerySubRedditImageUrl =	"gallery/r/{0}/{1}";
		internal const string GalleryRandomUrl =			"gallery/random/{0}";
		internal const string GallerySubRedditUrl =			"gallery/r/{0}/{1}/{2}/{3}";
		internal const string GallerySearchUrl =			"gallery/search/{0}/{1}?q={2}";
		internal const string GalleryUrl =					"gallery/{0}/{1}/{2}/{3}?showViral={4}";

		internal const string SubmitGalleryUrl =			"gallery/{0}";
		internal const string SubmitGalleryImageUrl =		"gallery/image/{0}";
		internal const string SubmitGalleryAlbumUrl =		"gallery/album/{0}";

		#endregion

		public GalleryEndpoint(Imgur imgurClient)
		{
			ImgurClient = imgurClient;
		}

		/// <summary>
		/// Returns the images in the gallery
		/// </summary>
		/// <param name="page">The current page</param>
		/// <param name="section">The section of the site</param>
		/// <param name="sort">The sort method</param>
		/// <param name="window">Change the date range of the request if the section is "top"</param>
		/// <param name="showVirual">Show or hide viral images from the 'user' section.</param>
		public async Task<ImgurResponse<IGalleryObject[]>> GetGalleryImagesAsync(int page = 0,
			GallerySection section = GallerySection.Hot, GallerySort sort = GallerySort.Viral,
			GalleryWindow window = GalleryWindow.Day, bool showVirual = true)
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			var endpoint = String.Format(GalleryUrl, section.ToString().ToLowerInvariant(), sort.ToString().ToLowerInvariant(),
				window.ToString().ToLowerInvariant(), page, showVirual.ToString().ToLowerInvariant());

			return
				await
					Request.SubmitImgurRequestAsync(Request.HttpMethod.Get, endpoint, ImgurClient.Authentication,
						customParser: ParseGalleryObjectArrayResponse);
		}

		/// <summary>
		/// View gallery images for a sub-reddit
		/// </summary>
		/// <param name="subreddit">A valid sub-reddit name</param>
		/// <param name="page">The current page</param>
		/// <param name="sort">The sort method (can't be viral)</param>
		/// <param name="window">Change the date range of the request if the section is "top"</param>
		public async Task<ImgurResponse<IGalleryObject[]>> GetSubredditGalleryAsync(string subreddit, int page = 0,
			GallerySort sort = GallerySort.Time, GalleryWindow window = GalleryWindow.Week)
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			var endpoint = String.Format(GallerySubRedditUrl, subreddit, sort.ToString().ToLowerInvariant(),
				window.ToString().ToLowerInvariant(), page);

			return
				await
					Request.SubmitImgurRequestAsync(Request.HttpMethod.Get, endpoint, ImgurClient.Authentication,
						customParser: ParseGalleryObjectArrayResponse);
		}

		/// <summary>
		/// View a single image in the subreddit
		/// </summary>
		/// <param name="subreddit">A valid sub-reddit name</param>
		/// <param name="imageId">The ID for the image</param>
		public async Task<ImgurResponse<GalleryImage>> GetSubredditGalleryImageAsync(string subreddit, string imageId)
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			var endpoint = String.Format(GallerySubRedditImageUrl, subreddit, imageId);

			return
				await
					Request.SubmitImgurRequestAsync<GalleryImage>(Request.HttpMethod.Get, endpoint, ImgurClient.Authentication);
		}

		/// <summary>
		/// Search the gallery with a given query string
		/// </summary>
		/// <param name="searchQuery">A valid search query</param>
		/// <param name="page">The current page</param>
		/// <param name="sort">The sort method (can't be viral)</param>
		public async Task<ImgurResponse<IGalleryObject[]>> SearchGalleryAsync(string searchQuery, int page = 0,
			GallerySort sort = GallerySort.Time)
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			var endpoint = String.Format(GallerySearchUrl, sort.ToString().ToLowerInvariant(), page, searchQuery);

			return
				await
					Request.SubmitImgurRequestAsync(Request.HttpMethod.Get, endpoint, ImgurClient.Authentication,
						customParser: ParseGalleryObjectArrayResponse);
		}

		/// <summary>
		/// Returns a random set of gallery images.
		/// </summary>
		/// <param name="page">A page of random gallery images, from 0-50. Pages are regenerated every hour.</param>
		public async Task<ImgurResponse<IGalleryObject[]>> GetRandomGalleryImagesAsync(int page = 0)
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			var endpoint = String.Format(GalleryRandomUrl, page);

			return
				await
					Request.SubmitImgurRequestAsync(Request.HttpMethod.Get, endpoint, ImgurClient.Authentication,
						customParser: ParseGalleryObjectArrayResponse);
		}
		
		#region Gallery Submission

		/// <summary>
		/// Add an Image to the Gallery.
		/// </summary>
		/// <param name="imageId">The ImageId of the image to add to the gallery</param>
		/// <param name="title">The title of the gallery post</param>
		public async Task<ImgurResponse<Boolean>> SubmitImageToGalleryAsync(string imageId, string title)
		{
			return await SubmitItemToGalleryAsync(imageId, title, false);
		}

		/// <summary>
		/// Add an Album to the Gallery.
		/// </summary>
		/// <param name="albumId">The AlbumId of the album to add to the gallery</param>
		/// <param name="title">The title of the gallery post</param>
		public async Task<ImgurResponse<Boolean>> SubmitAlbumToGalleryAsync(string albumId, string title)
		{
			return await SubmitItemToGalleryAsync(albumId, title, true);
		}

		/// <summary>
		/// Add an Album or Image to the Gallery.
		/// </summary>
		/// <param name="ident">The Id of the content to add to the gallery</param>
		/// <param name="title">The title of the gallery post</param>
		/// <param name="isAlbum">Indicates if the item is an album or image</param>
		private async Task<ImgurResponse<Boolean>> SubmitItemToGalleryAsync(string ident, string title, bool isAlbum)
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (!(ImgurClient.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			var endpoint = String.Format(isAlbum ? SubmitGalleryAlbumUrl : SubmitGalleryImageUrl, ident);

			var keyPairs = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("title", title),
				new KeyValuePair<string, string>("terms", "1")
			};
			var content = new FormUrlEncodedContent(keyPairs);

			return
				await
					Request.SubmitImgurRequestAsync<Boolean>(Request.HttpMethod.Post, endpoint, ImgurClient.Authentication, content: content);
		}

		#endregion

		/// <summary>
		/// Remove an image from the gallery.
		/// </summary>
		/// <param name="galleryId">The Id of the item to remove from the gallery</param>
		public async Task<ImgurResponse<Boolean>> RemoveItemFromGalleryAsync(string galleryId)
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (!(ImgurClient.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			return
				await
					Request.SubmitImgurRequestAsync<Boolean>(Request.HttpMethod.Delete, String.Format(GalleryRemovalUrl, galleryId), ImgurClient.Authentication);
		}

		#region Seralization Helpers

		/// <summary>
		/// Parses a list of Gallery Objects
		/// </summary>
		/// <param name="jsonObject">The <see cref="JObject"/> response from imgur</param>
		public ImgurResponse<IGalleryObject[]> ParseGalleryObjectArrayResponse(JObject jsonObject)
		{
			var imgurResponse = new ImgurResponse<IGalleryObject[]>
			{
				Success = jsonObject.Value<bool>("success"),
				Status = (HttpStatusCode) jsonObject.Value<Int32>("status")
			};

			var galleryObjects = new List<IGalleryObject>();
			foreach (var child in jsonObject.SelectToken("data").Children())
			{
				if (child.Value<bool>("is_album"))
					galleryObjects.Add(JsonConvert.DeserializeObject<GalleryAlbum>(child.ToString()));
				else
					galleryObjects.Add(JsonConvert.DeserializeObject<GalleryImage>(child.ToString()));
			}

			imgurResponse.Data = galleryObjects.ToArray();
			return imgurResponse;
		}

		/// <summary>
		/// Parses a Gallery Object
		/// </summary>
		/// <param name="jsonObject">The <see cref="JObject"/> response from imgur</param>
		public ImgurResponse<IGalleryObject> ParseGalleryObjectResponse(JObject jsonObject)
		{
			var imgurResponse = new ImgurResponse<IGalleryObject>
			{
				Success = jsonObject.Value<bool>("success"),
				Status = (HttpStatusCode)jsonObject.Value<Int32>("status")
			};

			if (jsonObject.SelectToken("data").Value<bool>("is_album"))
				imgurResponse.Data = JsonConvert.DeserializeObject<GalleryAlbum>(jsonObject.SelectToken("data").ToString());
			else
				imgurResponse.Data = JsonConvert.DeserializeObject<GalleryImage>(jsonObject.SelectToken("data").ToString());

			return imgurResponse;
		}

		#endregion
	}
}
