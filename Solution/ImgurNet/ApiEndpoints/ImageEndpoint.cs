using System;
using System.Threading.Tasks;
using ImgurNet.Exceptions;
using ImgurNet.Models;
using ImgurNet.Web;

namespace ImgurNet.ApiEndpoints
{
	public class ImageEndpoint
	{
		#region EndPoints

		private const string _imageUrl = "image/{0}";

		#endregion

		/// <summary>
		/// 
		/// </summary>
		public Imgur Imgur { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="imgur"></param>
		public ImageEndpoint(Imgur imgur)
		{
			Imgur = imgur;
		}

		/// <summary>
		/// Get information about an image.
		/// </summary>
		/// <param name="imageId">The Id of the image you want details of.</param>
		/// <returns>The image data.</returns>
		public async Task<ImgurResponse<Image>> GetImageDetails(string imageId)
		{
			if (Imgur.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			return await Request.Get<Image>(String.Format(_imageUrl, imageId), Imgur.Authentication);
		}
	}
}
