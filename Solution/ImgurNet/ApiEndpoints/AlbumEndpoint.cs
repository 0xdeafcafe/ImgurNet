using System;
using System.Threading.Tasks;
using ImgurNet.Exceptions;
using ImgurNet.Models;
using ImgurNet.Web;

namespace ImgurNet.ApiEndpoints
{
	public class AlbumEndpoint
	{
		#region EndPoints

		internal const string AlbumUrl = "album/{0}";
		internal const string AlbumImagesUrl = "album/{0}/images";
		internal const string AlbumImageUrl = "album/{0}/image/{1}";

		#endregion

		/// <summary>
		/// 
		/// </summary>
		public Imgur Imgur { get; private set; }

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
					Request.SubmitRequestAsync<Album>(Request.HttpMethod.Get, String.Format(AlbumUrl, albumId), Imgur.Authentication);
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
					Request.SubmitRequestAsync<Image[]>(Request.HttpMethod.Get, String.Format(AlbumImagesUrl, albumId), Imgur.Authentication);
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
					Request.SubmitRequestAsync<Image>(Request.HttpMethod.Get, String.Format(AlbumImageUrl, albumId, imageId), Imgur.Authentication);
		}
	}
}
