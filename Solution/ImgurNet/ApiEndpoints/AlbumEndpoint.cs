using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ImgurNet.Authentication;
using ImgurNet.Exceptions;
using ImgurNet.Models;
using ImgurNet.Web;

namespace ImgurNet.ApiEndpoints
{
	public class AlbumEndpoint : BaseEndpoint
	{
		#region EndPoints

		internal const string CreateAlbumUrl =		"album/";
		internal const string AlbumUrl =			"album/{0}";
		internal const string AlbumAddImagesUrl =	"album/{0}/add";
		internal const string AlbumImagesUrl =		"album/{0}/images";
		internal const string AlbumFavouriteUrl =	"album/{0}/favorite";
		internal const string AlbumImageUrl =		"album/{0}/image/{1}";
		internal const string AlbumRemoveImagesUrl ="album/{0}/remove_images";

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="imgur"></param>
		public AlbumEndpoint(Imgur imgur)
		{
			Imgur = imgur;
		}

		/// <summary>
		/// Get information about a specific album.
		/// </summary>
		/// <param name="albumId">The id of the album you are requesting.</param>
		/// <returns>The album data</returns>
		public async Task<ImgurResponse<Album>> GetAlbumDetailsAsync(string albumId)
		{
			if (Imgur.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			return
				await
					Request.SubmitImgurRequestAsync<Album>(Request.HttpMethod.Get, String.Format(AlbumUrl, albumId), Imgur.Authentication);
		}

		/// <summary>
		/// Get a list of images in a specific album.
		/// </summary>
		/// <param name="albumId">The id of the album you are requesting images from.</param>
		/// <returns>The album data</returns>
		public async Task<ImgurResponse<Image[]>> GetAllImagesFromAlbumAsync(string albumId)
		{
			if (Imgur.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			return
				await
					Request.SubmitImgurRequestAsync<Image[]>(Request.HttpMethod.Get, String.Format(AlbumImagesUrl, albumId), Imgur.Authentication);
		}

		#region Album Creation

		/// <summary>
		/// Creates an album on imgur, if authorized with <see cref="OAuth2Authentication"/> then it will be added to the authorised.
		/// </summary>
		/// <param name="images">A collection of valid Image's that should be added to this album.</param>
		/// <param name="coverImage">An Imgur Image that want to be the cover of the album (has to be in <see cref="images"/>)</param>
		/// <param name="title">The title of the album</param>
		/// <param name="description">The description of the album</param>
		/// <param name="privacy">The privacy mode of the album</param>
		/// <param name="layout">The defualt layout of the album</param>
		public async Task<ImgurResponse<Album>> CreateAlbumAsync(Image[] images = null, Image coverImage = null, string title = null, string description = null, Enums.Privacy privacy = Enums.Privacy.Public, Enums.AlbumLayout layout = Enums.AlbumLayout.Blog)
		{
			return await CreateAlbumFromIdsAsync(images == null ? null : images.Select(i => i.Id).ToArray(), (coverImage == null ? null : coverImage.Id), title, description, privacy, layout);
		}

		/// <summary>
		/// Creates an album on imgur, if authorized with <see cref="OAuth2Authentication"/> then it will be added to the authorised.
		/// </summary>
		/// <param name="imageIds">A collection of ImageId's for valid imgur images that should be added to this album.</param>
		/// <param name="coverImageId">The ImageId of the image you want to be the cover of the album (has to be in <see cref="imageIds"/>)</param>
		/// <param name="title">The title of the album</param>
		/// <param name="description">The description of the album</param>
		/// <param name="privacy">The privacy mode of the album</param>
		/// <param name="layout">The defualt layout of the album</param>
		public async Task<ImgurResponse<Album>> CreateAlbumFromIdsAsync(string[] imageIds = null, string coverImageId = null, string title = null, string description = null, Enums.Privacy privacy = Enums.Privacy.Public, Enums.AlbumLayout layout = Enums.AlbumLayout.Blog)
		{
			if (Imgur.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			var keyPairs = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("privacy", privacy.ToString().ToLowerInvariant()),
				new KeyValuePair<string, string>("layout", layout.ToString().ToLowerInvariant())
			};
			if (imageIds != null) keyPairs.Add(new KeyValuePair<string, string>("ids", String.Join(",", imageIds)));
			if (coverImageId != null) keyPairs.Add(new KeyValuePair<string, string>("cover", coverImageId));
			if (title != null) keyPairs.Add(new KeyValuePair<string, string>("title", title));
			if (description != null) keyPairs.Add(new KeyValuePair<string, string>("description", description));
			var multi = new FormUrlEncodedContent(keyPairs.ToArray());

			var album = await Request.SubmitImgurRequestAsync<Album>(Request.HttpMethod.Post, CreateAlbumUrl, Imgur.Authentication, content: multi);
			return await GetAlbumDetailsAsync(album.Data.Id);
		}

		#endregion

		/// <summary>
		/// Delete an album with a deletion hash
		/// </summary>
		/// <param name="albumDeletionHash">The deletion hash of the album</param>
		/// <returns></returns>
		public async Task<ImgurResponse<Boolean>> DeleteAlbumAsync(string albumDeletionHash)
		{
			if (Imgur.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			return
				await
					Request.SubmitImgurRequestAsync<Boolean>(Request.HttpMethod.Delete, String.Format(AlbumUrl, albumDeletionHash), Imgur.Authentication);
		}

		/// <summary>
		/// Adds/Removes an album from the authenticated user's favourites. Must be authenticated using <see cref="OAuth2Authentication"/> to call this Endpoint.
		/// </summary>
		/// <param name="albumId">The AlbumId of the album you want to favourite.</param>
		/// <returns>An bool declaring if the item is now favourited.</returns>
		public async Task<ImgurResponse<Boolean>> FavouriteAlbumAsync(string albumId)
		{
			if (Imgur.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (!(Imgur.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			var response =
				await
					Request.SubmitImgurRequestAsync<String>(Request.HttpMethod.Post, String.Format(AlbumFavouriteUrl, albumId),
						Imgur.Authentication);

			return new ImgurResponse<Boolean>
			{
				Data = (response.Data.ToLowerInvariant() == "favorited"),
				Status = response.Status,
				Success = response.Success
			};
		}

		#region Add Images to Album

		/// <summary>
		/// Adds an image to an album. Must be authenticated using <see cref="OAuth2Authentication"/> to call this Endpoint
		/// </summary>
		/// <param name="albumId">The AlbumId of the album you want to add an image to</param>
		/// <param name="imageId">An image id to add to the album</param>
		public async Task<ImgurResponse<Boolean>> AddImageToAlbumAsync(string albumId, string imageId)
		{
			return await AddImagesToAlbumAsync(albumId, new [] {imageId});
		}

		/// <summary>
		/// Adds images to an album. Must be authenticated using <see cref="OAuth2Authentication"/> to call this Endpoint
		/// </summary>
		/// <param name="albumId">The AlbumId of the album you want to add images to</param>
		/// <param name="imageIds">A collection of image ids to add to the album</param>
		public async Task<ImgurResponse<Boolean>> AddImagesToAlbumAsync(string albumId, string[] imageIds)
		{
			if (Imgur.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (!(Imgur.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			var keyPairs = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("ids", String.Join(",", imageIds))
			};
			var multi = new FormUrlEncodedContent(keyPairs.ToArray());

			return
				await
					Request.SubmitImgurRequestAsync<Boolean>(Request.HttpMethod.Post, String.Format(AlbumAddImagesUrl, albumId),
						Imgur.Authentication, content: multi);
		}

		#endregion

		#region Remove Images from Album

		/// <summary>
		/// Removes an image from an album. Must be authenticated using <see cref="OAuth2Authentication"/> to call this Endpoint
		/// </summary>
		/// <param name="albumId">The AlbumId of the album you want to remove an image from</param>
		/// <param name="imageId">An image id to remove from the album</param>
		public async Task<ImgurResponse<Boolean>> RemoveImageFromAlbumAsync(string albumId, string imageId)
		{
			return await RemoveImagesFromAlbumAsync(albumId, new[] {imageId});
		}

		/// <summary>
		/// Removes images from an album. Must be authenticated using <see cref="OAuth2Authentication"/> to call this Endpoint
		/// </summary>
		/// <param name="albumId">The AlbumId of the album you want to remove images from</param>
		/// <param name="imageIds">A collection of image ids to remove from the album</param>
		public async Task<ImgurResponse<Boolean>> RemoveImagesFromAlbumAsync(string albumId, string[] imageIds)
		{
			if (Imgur.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (!(Imgur.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			var keyPairs = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("ids", String.Join(",", imageIds))
			};
			var multi = new FormUrlEncodedContent(keyPairs.ToArray());

			return
				await
					Request.SubmitImgurRequestAsync<Boolean>(Request.HttpMethod.Delete, String.Format(AlbumRemoveImagesUrl, albumId),
						Imgur.Authentication, content: multi);
		}

		#endregion
	}
}
