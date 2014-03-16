using System.Threading.Tasks;
using ImgurNet.Authentication;
using ImgurNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImgurNet.Tests.Authentication
{
	[TestClass]
	public class OAuth2AuthenticationTests
	{
		[TestMethod]
		public async Task TestPinAuth()
		{
			// Create a new OAuth2 Authentication
			var oAuth2Authentication = new OAuth2Authentication("8db03472c3a6e93", "01d1e791eb9616f7cbb3e01f81691ea5d316f57e", false);
			var authorizationUrl = oAuth2Authentication.CreateAuthorizationUrl(Enums.OAuth2Type.Pin, "dicks");
			var pin = "1234";
			await oAuth2Authentication.AuthorizeWithPin(pin);
		}

		[TestMethod]
		public async Task TestCodeAuth()
		{
			// Create a new OAuth2 Authentication
			var oAuth2Authentication = new OAuth2Authentication("8db03472c3a6e93", "01d1e791eb9616f7cbb3e01f81691ea5d316f57e", false);
			var authorizationUrl = oAuth2Authentication.CreateAuthorizationUrl(Enums.OAuth2Type.Code, "dicks");
			var code = "1234";
			await oAuth2Authentication.AuthorizeWithCode(code);
		}
	}
}
