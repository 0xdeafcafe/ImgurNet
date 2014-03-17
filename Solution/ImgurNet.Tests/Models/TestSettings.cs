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
	}
}
