using System;
using System.Threading.Tasks;
using ImgurNet.Exceptions;
using ImgurNet.Models;
using ImgurNet.Web;

namespace ImgurNet.ApiEndpoints
{
	public class AccountEndpoint
	{
		#region EndPoints

		internal const string AccountUrl = "account/{0}";

		#endregion

		/// <summary>
		/// 
		/// </summary>
		public Imgur Imgur { get; private set; }

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
		public async Task<ImgurResponse<Account>> GetAccountDetails(string username)
		{
			if (Imgur.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			return await Request.SubmitRequest<Account>(Request.HttpMethod.Get, String.Format(AccountUrl, username), Imgur.Authentication);
		}
	}
}
