using System;
using System.Threading.Tasks;
using ImgurNet.Authentication;
using ImgurNet.Exceptions;
using ImgurNet.Models;
using ImgurNet.Web;

namespace ImgurNet.ApiEndpoints
{
	public class AccountEndpoint : BaseEndpoint
	{
		#region EndPoints

		internal const string AccountUrl =					"account/{0}";
		internal const string AccountImageCountUrl =		"account/{0}/images/count";

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="imgur"></param>
		public AccountEndpoint(Imgur imgur)
		{
			Imgur = imgur;
		}

		/// <summary>
		/// Request standard account information
		/// </summary>
		/// <param name="username">The username of the account you want information of.</param>
		/// <returns>The account data</returns>
		public async Task<ImgurResponse<Account>> GetAccountDetailsAsync(string username)
		{
			if (Imgur.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			return await Request.SubmitImgurRequestAsync<Account>(Request.HttpMethod.Get, String.Format(AccountUrl, username), Imgur.Authentication);
		}

		/// <summary>
		/// Gets the number of images the user has uploaded.
		/// </summary>
		/// <param name="username">The username to get image count from. Can be null if using OAuth2, and it will use that account to get the details.</param>
		public async Task<ImgurResponse<int>> GetAccountImageCount(string username = null)
		{
			if (Imgur.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (username == null)
				if (Imgur.Authentication is OAuth2Authentication)
					username = "me";
				else
					throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			return
				await
					Request.SubmitImgurRequestAsync<int>(Request.HttpMethod.Get, String.Format(AccountImageCountUrl, username),
						Imgur.Authentication);
		}
	}
}
