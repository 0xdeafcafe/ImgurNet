using System.Threading.Tasks;
using ImgurNet.Authentication;
using ImgurNet.Tests.Models;

namespace ImgurNet.Tests.Helpers
{
	public static class OAuthHelpers
	{
		public static async Task<OAuth2Authentication> GetAccessToken(OAuth2Authentication authentication, TestSettings settings)
		{
			authentication.AuthorizeWithToken(settings.AccessToken, settings.RefreshToken, 3600, settings.AuthorizedUsername);
			await authentication.RefreshTokens();

			settings.AccessToken = authentication.AccessToken;
			settings.RefreshToken = authentication.RefreshToken;
			VariousFunctions.SaveTestSettings(settings);

			return authentication;
		}
	}
}
