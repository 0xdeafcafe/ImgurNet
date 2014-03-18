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
		internal const string AlbumImagesUrl =		"album/{0}/images";
		internal const string AlbumImageUrl =		"album/{0}/image/{1}";

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

		/// <summary>
		/// Get a specific image from a specific album.
		/// </summary>
		/// <param name="albumId">The id of the album you are requesting images from.</param>
		/// <param name="imageId">The id of the image you want to get from the album.</param>
		/// <returns>The album data</returns>
		public async Task<ImgurResponse<Image>> GetImageFromAlbumAsync(string albumId, string imageId)
		{
			if (Imgur.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			return
				await
					Request.SubmitImgurRequestAsync<Image>(Request.HttpMethod.Get, String.Format(AlbumImageUrl, albumId, imageId), Imgur.Authentication);
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
		/// 
		/// </summary>
		/// <param name="albumDeletionHash"></param>
		/// <returns></returns>
		public async Task<ImgurResponse<Boolean>> DeleteAlbumAsync(string albumDeletionHash)
		{
			if (Imgur.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			return
				await
					Request.SubmitImgurRequestAsync<Boolean>(Request.HttpMethod.Delete, String.Format(AlbumUrl, albumDeletionHash), Imgur.Authentication);
		}
	}
}
