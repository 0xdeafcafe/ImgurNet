using Newtonsoft.Json;

namespace ImgurNet.Models
{
	public class OAuthTokens : NotifyPropertyChangedBase
	{
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("access_token")]
		public string AccessToken
		{
			get { return _accessToken; }
			set { SetField(ref _accessToken, value); }
		}
		private string _accessToken;

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("expires_in")]
		public int ExpiresIn
		{
			get { return _expiresIn; }
			set { SetField(ref _expiresIn, value); }
		}
		private int _expiresIn;

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("token_type")]
		public string TokenType
		{
			get { return _tokenType; }
			set { SetField(ref _tokenType, value); }
		}
		private string _tokenType;

		/// <summary>
		/// 
		/// </summary>
		public string Scope
		{
			get { return _scope; }
			set { SetField(ref _scope, value); }
		}
		private string _scope;

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("refresh_token")]
		public string RefreshToken
		{
			get { return _refreshToken; }
			set { SetField(ref _refreshToken, value); }
		}
		private string _refreshToken;

		/// <summary>
		/// Username of the account that is authorised
		/// </summary>
		[JsonProperty("account_username")]
		public string AuthorizedUsername
		{
			get { return _authorizedUsername; }
			set { SetField(ref _authorizedUsername, value); }
		}
		private string _authorizedUsername;
	}
}
