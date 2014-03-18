using System.Threading.Tasks;
using ImgurNet.Exceptions;
using ImgurNet.Models;
using ImgurNet.Web;

namespace ImgurNet.ApiEndpoints
{
	public class MemeGenEndpoint : BaseEndpoint
	{
		#region EndPoints

		internal const string DefaultMemesUrl = "memegen/defaults";

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="imgur"></param>
		public MemeGenEndpoint(Imgur imgur)
		{
			Imgur = imgur;
		}

		/// <summary>
		/// Get the list of default memes.
		/// </summary>
		public async Task<ImgurResponse<Image[]>> GetDefaultMemesAsync()
		{
			if (Imgur.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			return await Request.SubmitImgurRequestAsync<Image[]>(Request.HttpMethod.Get, DefaultMemesUrl, Imgur.Authentication);
		}
	}
}
