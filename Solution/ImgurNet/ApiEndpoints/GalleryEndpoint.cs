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

		internal const string GalleryDetailsUrl =			"gallery/{0}";
		internal const string GalleryImageDetailsUrl =		"gallery/image/{0}";
		internal const string GalleryAlbumDetailsUrl =		"gallery/album/{0}";

		internal const string GalleryReportUrl =			"gallery/{0}/report";
		internal const string GalleryReportImageUrl =		"gallery/image/{0}/report";
		internal const string GalleryReportAlbumUrl =		"gallery/album/{0}/report";

		internal const string GalleryVotesUrl =				"gallery/{0}/votes";
		internal const string GalleryVotesImageUrl =		"gallery/image/{0}/votes";
		internal const string GalleryVotesAlbumUrl =		"gallery/album/{0}/votes";

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

		#region Get Gallery Object

		/// <summary>
		/// Get additional information about an album in the gallery.
		/// </summary>
		/// <param name="albumId">The Id of the album to report</param>
		public async Task<ImgurResponse<GalleryAlbum>> GetGalleryAlbumAsync(string albumId)
		{
			return await GetGalleryObjectAsync<GalleryAlbum>(albumId, true);
		}

		/// <summary>
		/// Get additional information about an image in the gallery.
		/// </summary>
		/// <param name="imageId">The Id of the image to report</param>
		public async Task<ImgurResponse<GalleryImage>> GetGalleryImageAsync(string imageId)
		{
			return await GetGalleryObjectAsync<GalleryImage>(imageId, false);
		}
		
		/// <summary>
		/// Get additional information about an item in the gallery.
		/// </summary>
		/// <typeparam name="T">The specified Gallery Object</typeparam>
		/// <param name="ident">The Id of the item to get</param>
		/// <param name="isAlbum">Flags declaring if the item is an album</param>
		private async Task<ImgurResponse<T>> GetGalleryObjectAsync<T>(string ident, bool isAlbum)
			where T : IGalleryObject
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (!(ImgurClient.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			var endpoint = String.Format(isAlbum ? GalleryAlbumDetailsUrl : GalleryImageDetailsUrl, ident);

			return
				await
					Request.SubmitImgurRequestAsync<T>(Request.HttpMethod.Get, endpoint, ImgurClient.Authentication);
		}

		#endregion

		#region Report Gallery Object

		/// <summary>
		/// Report an Album in the gallery
		/// </summary>
		/// <param name="albumId">The Id of the album to report</param>
		public async Task<ImgurResponse<Boolean>> ReportGalleryAlbumAsync(string albumId)
		{
			return await ReportGalleryObjectAsync(albumId, true);
		}

		/// <summary>
		/// Report an Image in the gallery
		/// </summary>
		/// <param name="imageId">The Id of the image to report</param>
		public async Task<ImgurResponse<Boolean>> ReportGalleryImageAsync(string imageId)
		{
			return await ReportGalleryObjectAsync(imageId, false);
		}

		/// <summary>
		/// Report an Item in the gallery
		/// </summary>
		/// <typeparam name="T">The specified Gallery Object</typeparam>
		/// <param name="ident">The Id of the item to report</param>
		/// <param name="isAlbum">Flags declaring if the item is an album</param>
		private async Task<ImgurResponse<Boolean>> ReportGalleryObjectAsync(string ident, bool isAlbum)
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (!(ImgurClient.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			var endpoint = String.Format(isAlbum ? GalleryReportAlbumUrl : GalleryReportImageUrl, ident);

			return
				await
					Request.SubmitImgurRequestAsync<Boolean>(Request.HttpMethod.Delete, endpoint, ImgurClient.Authentication);
		}

		#endregion

		#region Get Galery Object Votes

		/// <summary>
		/// Get votes for an Album in the gallery
		/// </summary>
		/// <param name="albumId">The Id of the album to get votes for</param>
		public async Task<ImgurResponse<Vote>> GetGalleryAlbumVotesAsync(string albumId)
		{
			return await GetGalleryObjectVotesAsync(albumId, true);
		}

		/// <summary>
		/// Get votes for an Image in the gallery
		/// </summary>
		/// <param name="imageId">The Id of the image to get votes for</param>
		public async Task<ImgurResponse<Vote>> GetGalleryImageVotesAsync(string imageId)
		{
			return await GetGalleryObjectVotesAsync(imageId, false);
		}

		/// <summary>
		/// Get votes for an Item in the gallery
		/// </summary>
		/// <param name="ident">The Id of the item to get votes for</param>
		/// <param name="isAlbum">Flags declaring if the item is an album</param>
		private async Task<ImgurResponse<Vote>> GetGalleryObjectVotesAsync(string ident, bool isAlbum)
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (!(ImgurClient.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			var endpoint = String.Format(isAlbum ? GalleryVotesAlbumUrl : GalleryVotesImageUrl, ident);

			return
				await
					Request.SubmitImgurRequestAsync<Vote>(Request.HttpMethod.Get, endpoint, ImgurClient.Authentication);
		}

		#endregion

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
