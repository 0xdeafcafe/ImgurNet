using System.Net;
using System.Threading.Tasks;
using ImgurNet.ApiEndpoints;
using ImgurNet.Authentication;
using ImgurNet.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImgurNet.Tests.ApiEndpoints
{
	[TestClass]
	public class MemeGenEndpointTests
	{
		[TestMethod]
		public async Task TestGetImageDetails()
		{
			var settings = VariousFunctions.LoadTestSettings();

			var imgurClient = new Imgur(new ClientAuthentication(settings.ClientId, false));
			var memeGenEndpoint = new MemeGenEndpoint(imgurClient);
			var response = await memeGenEndpoint.GetDefaultMemes();

			// Assert the Reponse
			Assert.IsNotNull(response.Data);
			Assert.AreEqual(response.Success, true);
			Assert.AreEqual(response.Status, HttpStatusCode.OK);
		}
	}
}
