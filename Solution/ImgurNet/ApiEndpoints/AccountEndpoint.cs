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
		internal const string AccountDeleteImageUrl =		"account/{0}/image/{1}";
		internal const string AccountImageDetailsUrl =		"account/{0}/image/{1}";
		internal const string AccountImageIdsUrl =			"account/{0}/images/ids";
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
		/// Gets the Details of an image uploaded by an account.
		/// </summary>
		/// <param name="imageId">The Id of the image to get details from</param>
		/// <param name="username">The username to get the image from. Can be ignored if using OAuth2, and it will use that account.</param>
		public async Task<ImgurResponse<Image>> GetAccountImageDetails(string imageId, string username = "me")
		{
			if (Imgur.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (username == "me" && !(Imgur.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			return
				await
					Request.SubmitImgurRequestAsync<Image>(Request.HttpMethod.Get, String.Format(AccountImageDetailsUrl, username, imageId),
						Imgur.Authentication);
		}

		/// <summary>
		/// Gets a list of ImageId's that the account has uploaded
		/// </summary>
		/// <param name="username">The username to get image count from. Can be ignored if using OAuth2, and it will use that account.</param>
		public async Task<ImgurResponse<String[]>> GetAccountImageIds(string username = "me")
		{
			if (Imgur.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (username == "me" && !(Imgur.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			return
				await
					Request.SubmitImgurRequestAsync<String[]>(Request.HttpMethod.Get, String.Format(AccountImageIdsUrl, username),
						Imgur.Authentication);
		}

		/// <summary>
		/// Gets the number of images the user has uploaded.
		/// </summary>
		/// <param name="username">The username to get image count from. Can be ignored if using OAuth2, and it will use that account.</param>
		public async Task<ImgurResponse<int>> GetAccountImageCount(string username = "me")
		{
			if (Imgur.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (username == "me" && !(Imgur.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			return
				await
					Request.SubmitImgurRequestAsync<int>(Request.HttpMethod.Get, String.Format(AccountImageCountUrl, username),
						Imgur.Authentication);
		}

		/// <summary>
		/// Deletes an image from a user's account. This always requires a deletion hash.
		/// </summary>
		/// <param name="deletionHash">The deletion hash for the image.</param>
		/// <param name="username">The username of the account to delete from. Can be ignored if using OAuth2, and it will use that account.</param>
		public async Task<ImgurResponse<Boolean>> DeleteAccountImage(string deletionHash, string username = "me")
		{
			if (Imgur.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (username == "me" && !(Imgur.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			return
				await
					Request.SubmitImgurRequestAsync<Boolean>(Request.HttpMethod.Delete, String.Format(AccountDeleteImageUrl, username, deletionHash),
						Imgur.Authentication);
		}
	}
}
