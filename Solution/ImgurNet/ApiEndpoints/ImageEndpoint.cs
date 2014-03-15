using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ImgurNet.Exceptions;
using ImgurNet.Extensions;
using ImgurNet.Models;
using ImgurNet.Web;

namespace ImgurNet.ApiEndpoints
{
	public class ImageEndpoint
	{
		#region EndPoints

		private const string ImageUrl = "image/{0}";
		private const string UploadImageUrl = "upload";

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

			return await Request.SubmitRequest<Image>(Request.HttpMethod.Get, String.Format(ImageUrl, imageId), Imgur.Authentication);
		}

		#region Upload Base64 Image

		public async Task<ImgurResponse<Image>> UploadImageFromBase64(string base64ImageData,
			string albumId = null, string name = null, string title = null, string description = null)
		{
			if (Imgur.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			// Create query string
			var nameCollection = new Dictionary<string, string>
			{
				{ "image", base64ImageData },
				{ "type", "base64" }
			};
			if (albumId != null) nameCollection.Add("album", albumId);
			if (name != null) nameCollection.Add("name", name);
			if (title != null) nameCollection.Add("title", title);
			if (description != null) nameCollection.Add("description", description);

			return await Request.SubmitRequest<Image>(Request.HttpMethod.Post, UploadImageUrl, Imgur.Authentication,
				content: new StringContent(nameCollection.ToStringCollection()));
		}

		#endregion
	}
}
