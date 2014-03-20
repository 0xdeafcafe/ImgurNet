using System.Threading.Tasks;
using ImgurNet.Authentication;
using ImgurNet.Exceptions;
using ImgurNet.Models;
using ImgurNet.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImgurNet.Tests.Authentication
{
	[TestClass]
	public class OAuth2AuthenticationTests
	{
		[TestMethod]
		public async Task TestPinAuth()
		{
			var settings = VariousFunctions.LoadTestSettings();

			// Create a new OAuth2 Authentication
			var oAuth2Authentication = new OAuth2Authentication(settings.ClientId, settings.ClientSecret, false);
			var authorizationUrl = oAuth2Authentication.CreateAuthorizationUrl(OAuth2Type.Pin, "dicks");
			var pin = "1234";
			try
			{
				await oAuth2Authentication.AuthorizeWithPin(pin);
			}
			catch (ImgurResponseFailedException exception)
			{
				Assert.AreEqual(exception.ImgurResponse.Data.ErrorDescription, "Invalid Pin");
			}
		}

		[TestMethod]
		public async Task TestCodeAuth()
		{
			var settings = VariousFunctions.LoadTestSettings();

			// Create a new OAuth2 Authentication
			var oAuth2Authentication = new OAuth2Authentication(settings.ClientId, settings.ClientSecret, false);
			var authorizationUrl = oAuth2Authentication.CreateAuthorizationUrl(OAuth2Type.Code, "dicks");
			var code = "1234";
			try
			{
				await oAuth2Authentication.AuthorizeWithCode(code);
			}
			catch (ImgurResponseFailedException exception)
			{
				Assert.AreEqual(exception.ImgurResponse.Data.ErrorDescription, "Refresh token doesn't exist or is invalid for the client");
			}
		}
	}
}
