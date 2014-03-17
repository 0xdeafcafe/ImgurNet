using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ImgurNet.Exceptions;
using ImgurNet.Extensions;
using ImgurNet.Helpers;
using ImgurNet.Models;
using ImgurNet.Web;

namespace ImgurNet.Authentication
{
	public class OAuth2Authentication : IAuthentication
	{
		/// <summary>
		/// Base Imgur Authorization endpoint
		/// </summary>
		internal const string AuthorizationEndpoint = "https://api.imgur.com/oauth2/authorize";

		/// <summary>
		/// Token generation endpoint
		/// </summary>
		internal const string TokenEndpoint = "https://api.imgur.com/oauth2/token";

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

		/// <summary>
		/// 
		/// </summary>
		public string AuthorizedUsername { get; private set; }

		#region OAuth Stuff

		/// <summary>
		/// Used to authenticate requests
		/// </summary>
		public string AccessToken { get; private set; }

		/// <summary>
		/// Used to generate a new token set when the <see cref="AccessToken"/> has expired.
		/// </summary>
		public string RefreshToken { get; private set; }

		/// <summary>
		/// When the <see cref="AccessToken"/> will expire, and you have to generate a new one with the refresh token
		/// </summary>
		public DateTime ExpiresAt { get; private set; }

		/// <summary>
		/// Checks to see if the <see cref="AccessToken"/> has Expires
		/// </summary>
		public bool HasExpired
		{
			get { return (ExpiresAt < DateTime.UtcNow); }
		}

		/// <summary>
		/// The type of OAuth authentication used to aquire the tokens
		/// </summary>
		public Enums.OAuth2Type OAuthType { get; private set; }

		#endregion

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
			AsyncHelper.RunSync(() => Request.SubmitImgurRequestAsync<Credits>(Request.HttpMethod.Get, Credits.CreditsUrl, this));
		}

		#region Authorizing

		/// <summary>
		///    Creates the Authorization Url, that the user will visit to allow your application to access their user account.
		///    For more information on how OAuth2 on Imgur works, visit their Developer Section (https://api.imgur.com/oauth2)
		/// </summary>
		/// <param name="responseType">
		///    The type of response you want to use. It's recomended that;
		///    - Pin: Desktop/Server/Mobile Applications
		///    - Code: Desktop/Server/Mobile Applications
		///    - Token: Javascript Applications
		/// </param>
		/// <param name="state">The state you want to pass through auth. This will be given back to you from the re-direct url if you use Code or Token Authentication.</param>
		public string CreateAuthorizationUrl(Enums.OAuth2Type responseType, string state = null)
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
		/// Authorizes with the tokens that you recieved from the url in <see cref="CreateAuthorizationUrl"/>
		/// </summary>
		/// <param name="accessToken">The <see cref="AccessToken"/> you recieved.</param>
		/// <param name="refreshToken">The <see cref="RefreshToken"/> you recieved.</param>
		/// <param name="expiresIn">The <see cref="ExpiresAt"/> you recieved.</param>
		/// <param name="authorizedUsername">The Username of the account that has been authorized.</param>
		public void AuthorizeWithToken(string accessToken, string refreshToken, int expiresIn, string authorizedUsername)
		{
			AccessToken = accessToken;
			RefreshToken = refreshToken;
			ExpiresAt = DateTime.UtcNow.AddSeconds(expiresIn);
			AuthorizedUsername = authorizedUsername;
		}

		/// <summary>
		/// Gets the token set from the pin the user has inputted. Throws a <exception cref="ImgurResponseFailedException" /> if the pin is not valid.
		/// </summary>
		/// <param name="pin">The pin the user entered.</param>
		public async Task AuthorizeWithPin(string pin)
		{
			var keyPairs = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("client_id", ClientId),
				new KeyValuePair<string, string>("client_secret", ClientSecret),
				new KeyValuePair<string, string>("grant_type", "pin"),
				new KeyValuePair<string, string>("pin", pin)
			};
			var multi = new FormUrlEncodedContent(keyPairs.ToArray());

			var tokens = await Request.SubmitGenericRequestAsync<OAuthTokens>(Request.HttpMethod.Post, TokenEndpoint, content: multi);

			AccessToken = tokens.AccessToken;
			RefreshToken = tokens.RefreshToken;
			ExpiresAt = DateTime.UtcNow.AddSeconds(tokens.ExpiresIn);
			AuthorizedUsername = tokens.AuthorizedUsername;
		}

		/// <summary>
		/// Gets the token set from the code in the query string of the callback url. Throws a <exception cref="ImgurResponseFailedException" /> if the code is not valid.
		/// </summary>
		/// <param name="code">The code from the callback Url.</param>
		public async Task AuthorizeWithCode(string code)
		{
			var keyPairs = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("client_id", ClientId),
				new KeyValuePair<string, string>("client_secret", ClientSecret),
				new KeyValuePair<string, string>("grant_type", "authorization_code"),
				new KeyValuePair<string, string>("code", code)
			};
			var multi = new FormUrlEncodedContent(keyPairs.ToArray());

			var tokens = await Request.SubmitGenericRequestAsync<OAuthTokens>(Request.HttpMethod.Post, TokenEndpoint, content: multi);

			AccessToken = tokens.AccessToken;
			RefreshToken = tokens.RefreshToken;
			ExpiresAt = DateTime.UtcNow.AddSeconds(tokens.ExpiresIn);
			AuthorizedUsername = tokens.AuthorizedUsername;
		}

		/// <summary>
		/// Gets a new <see cref="AccessToken"/> and <see cref="RefreshToken"/>
		/// </summary>
		public async Task RefreshTokens()
		{
			var keyPairs = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("client_id", ClientId),
				new KeyValuePair<string, string>("client_secret", ClientSecret),
				new KeyValuePair<string, string>("grant_type", "refresh_token"),
				new KeyValuePair<string, string>("refresh_token", RefreshToken)
			};
			var multi = new FormUrlEncodedContent(keyPairs.ToArray());

			var tokens = await Request.SubmitGenericRequestAsync<OAuthTokens>(Request.HttpMethod.Post, TokenEndpoint, content: multi);

			AccessToken = tokens.AccessToken;
			RefreshToken = tokens.RefreshToken;
			ExpiresAt = DateTime.UtcNow.AddSeconds(tokens.ExpiresIn);
			AuthorizedUsername = tokens.AuthorizedUsername;
		}

		#endregion

		/// <summary>
		/// This holds the current client's rate limit data
		/// </summary>
		public Credits RateLimit { get; internal set; }
		
		/// <summary>
		/// Generates a grant_type from the OAuth2 Type used in this authentication
		/// </summary>
		/// <param name="oAuth2Type"></param>
		private static string GrantTypeFromOAuthType(Enums.OAuth2Type oAuth2Type)
		{
			switch (oAuth2Type)
			{
				case Enums.OAuth2Type.Code:
					return "authorization_code";
				case Enums.OAuth2Type.Pin:
					return "pin";
				case Enums.OAuth2Type.Token:
					return "token";

				default:
					throw new ArgumentOutOfRangeException("oAuth2Type");
			}
		}
	}
}
