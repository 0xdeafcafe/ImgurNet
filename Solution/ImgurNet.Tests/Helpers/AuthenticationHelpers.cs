using System.Threading.Tasks;
using ImgurNet.Authentication;

namespace ImgurNet.Tests.Helpers
{
	public static class AuthenticationHelpers
	{
		public static Imgur CreateClientAuthenticatedImgurClient()
		{
			var settings = VariousFunctions.LoadTestSettings();
			return new Imgur(new ClientAuthentication(settings.ClientId, false));
		}

		public static async Task<Imgur> CreateOAuth2AuthenticatedImgurClient()
		{
			var settings = VariousFunctions.LoadTestSettings();
			var authentication = new OAuth2Authentication(settings.ClientId, settings.ClientSecret, false);
			await OAuthHelpers.GetAccessToken(authentication, settings);
			return new Imgur(authentication);
		}
	}
}
