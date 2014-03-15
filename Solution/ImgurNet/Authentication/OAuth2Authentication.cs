using System;
using System.Collections.Generic;
using ImgurNet.Extensions;
using ImgurNet.Helpers;
using ImgurNet.Models;
using ImgurNet.Web;

namespace ImgurNet.Authentication
{
	public class OAuth2Authentication : IAuthentication
	{
		/// <summary>
		/// 
		/// </summary>
		internal const string AuthorizationEndpoint = "https://api.imgur.com/oauth2/authorize";

		/// <summary>
		/// The authenticationType (always OAuth2 in this class).
		/// </summary>
		public AuthenticationType AuthenticationType { get; private set; }

		/// <summary>
		/// The ClientId of your Imgur application.
		/// </summary>
		public string ClientId { get; private set; }

		/// <summary>
		/// The ClientSecret of your Imgur application.
		/// </summary>
		public string ClientSecret { get; private set; }

		#region OAuth Stuff

		/// <summary>
		/// 
		/// </summary>
		public string AccessToken { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public string RefreshToken { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime ExpiresAt { get; private set; }

		/// <summary>
		/// 
		/// </summary>
		public bool HasExpired
		{
			get { return (ExpiresAt > DateTime.UtcNow); }
		}

		#endregion

		public Enums.OAuth2Type OAuthType { get; private set; }

		/// <summary>
		/// Creates a new Imgur Authorization based off of a client's ClientId.
		/// </summary>
		/// <param name="clientId">The ClientId of your imgur client. Create one here (https://api.imgur.com/oauth2/addclient).</param>
		/// <param name="clientSecret">The ClientSecret of your imgur client. Create one here (https://api.imgur.com/oauth2/addclient).</param>
		/// <param name="checkRateLimit">Check, and load into the model, the current ratelimit status of your client.</param>
		public OAuth2Authentication(string clientId, string clientSecret, bool checkRateLimit)
		{
			ClientId = clientId;
			ClientSecret = clientSecret;
			AuthenticationType = AuthenticationType.OAuth2;

			RateLimit = new Credits();
			if (!checkRateLimit) return;
			AsyncHelper.RunSync(() => Request.SubmitRequestAsync<Credits>(Request.HttpMethod.Get, Credits.CreditsUrl, this));
		}
		
		#region Authorizing

		/// <summary>
		/// 
		/// </summary>
		/// <param name="responseType"></param>
		/// <param name="state"></param>
		public string Authorization(Enums.OAuth2Type responseType, string state = null)
		{
			var queryStrings = new Dictionary<string, string> { { "client_id", ClientId } };
			if (state != null) queryStrings.Add("state", state);

			OAuthType = responseType;
			switch (responseType)
			{
				case Enums.OAuth2Type.Code:
					queryStrings.Add("response_type", "code");
					break;
				case Enums.OAuth2Type.Token:
					queryStrings.Add("response_type", "token");
					break;
				case Enums.OAuth2Type.Pin:
					queryStrings.Add("response_type", "pin");
					break;
				default:
					throw new InvalidOperationException();
			}

			return queryStrings.ToQueryString(AuthorizationEndpoint);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="accessToken"></param>
		/// <param name="refreshToken"></param>
		/// <param name="expiresIn"></param>
		public void AuthorizeWithToken(string accessToken, string refreshToken, int expiresIn)
		{
			AccessToken = accessToken;
			RefreshToken = refreshToken;
			ExpiresAt = DateTime.UtcNow.AddSeconds(expiresIn);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pin"></param>
		public void AuthorizeWithPin(int pin)
		{

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="code"></param>
		public void AuthorizeWithCode(string code)
		{
			
		}

		/// <summary>
		/// 
		/// </summary>
		public void RefreshTokens()
		{
			
		}

		#endregion

		/// <summary>
		/// This holds the current client's rate limit data
		/// </summary>
		public Credits RateLimit { get; internal set; }
	}
}
