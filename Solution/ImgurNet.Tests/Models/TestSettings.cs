using Newtonsoft.Json;

namespace ImgurNet.Tests.Models
{
	public class TestSettings
	{
		public string Username { get; set; }

		public string Password { get; set; }

		[JsonProperty("client_id")]
		public string ClientId { get; set; }

		[JsonProperty("client_secret")]
		public string ClientSecret { get; set; }

		[JsonProperty("access_token")]
		public string AccessToken { get; set; }

		[JsonProperty("refresh_token")]
		public string RefreshToken { get; set; }
	}
}
