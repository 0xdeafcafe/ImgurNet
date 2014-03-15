using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ImgurNet.Exceptions;
using ImgurNet.Models;
using ImgurNet.Web;

namespace ImgurNet.ApiEndpoints
{
	public class ImageEndpoint
	{
		internal const int MaxUriLength = 32766;

		#region EndPoints

		internal const string ImageUrl = "image/{0}";
		internal const string UploadImageUrl = "image";

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

			var sb = new StringBuilder();
			for (var i = 0; i < base64ImageData.Length; i += MaxUriLength)
				sb.Append(base64ImageData.Substring(i, Math.Min(MaxUriLength, base64ImageData.Length - i)));

			var keyPairs = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("image", base64ImageData),
				new KeyValuePair<string, string>("type", "base64")
			};
			if (albumId != null) keyPairs.Add(new KeyValuePair<string, string>(albumId, albumId));
			if (name != null) keyPairs.Add(new KeyValuePair<string, string>("name", name));
			if (title != null) keyPairs.Add(new KeyValuePair<string, string>("title", title));
			if (description != null) keyPairs.Add(new KeyValuePair<string, string>("description", description));
			var multi = new FormUrlEncodedContent(keyPairs.ToArray());

			return await Request.SubmitRequest<Image>(Request.HttpMethod.Post, UploadImageUrl, Imgur.Authentication, content: multi);
		}

		#endregion

		#region Upload Image From Url

		public async Task<ImgurResponse<Image>> UploadImageFromUrl(string url,
			string albumId = null, string name = null, string title = null, string description = null)
		{
			return await UploadImageFromUrl(new Uri(url), albumId, name, title, description);
		}

		public async Task<ImgurResponse<Image>> UploadImageFromUrl(Uri uri,
			string albumId = null, string name = null, string title = null, string description = null)
		{
			if (Imgur.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			var keyPairs = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("image", uri.ToString()),
				new KeyValuePair<string, string>("type", "url")
			};
			if (albumId != null) keyPairs.Add(new KeyValuePair<string, string>(albumId, albumId));
			if (name != null) keyPairs.Add(new KeyValuePair<string, string>("name", name));
			if (title != null) keyPairs.Add(new KeyValuePair<string, string>("title", title));
			if (description != null) keyPairs.Add(new KeyValuePair<string, string>("description", description));
			var multi = new FormUrlEncodedContent(keyPairs.ToArray());

			return await Request.SubmitRequest<Image>(Request.HttpMethod.Post, UploadImageUrl, Imgur.Authentication, content: multi);
		}

		#endregion

		#region Upload Image From Binary

		public async Task<ImgurResponse<Image>> UploadImageFromBinary(byte[] imageBinary,
			string albumId = null, string name = null, string title = null, string description = null)
		{
			return await UploadImageFromBase64(Convert.ToBase64String(imageBinary), albumId, name, title, description);
		}

		#endregion
	}
}
