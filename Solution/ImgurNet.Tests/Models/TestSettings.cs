using Newtonsoft.Json;

namespace ImgurNet.Tests.Models
{
	public class TestSettings
	{
		[JsonProperty("client_id")]
		public string ClientId { get; set; }

		[JsonProperty("client_secret")]
		public string ClientSecret { get; set; }

		[JsonProperty("access_token")]
		public string AccessToken { get; set; }

		[JsonProperty("refresh_token")]
		public string RefreshToken { get; set; }

		[JsonProperty("authorized_username")]
		public string AuthorizedUsername { get; set; }
	}
}
