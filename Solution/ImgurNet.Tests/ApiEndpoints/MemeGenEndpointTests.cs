using System.Net;
using System.Threading.Tasks;
using ImgurNet.ApiEndpoints;
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
			var imgurClient = AuthenticationHelpers.CreateClientAuthenticatedImgurClient();
			var memeGenEndpoint = new MemeGenEndpoint(imgurClient);
			var response = await memeGenEndpoint.GetDefaultMemesAsync();

			// Assert the Reponse
			Assert.IsNotNull(response.Data);
			Assert.AreEqual(response.Success, true);
			Assert.AreEqual(response.Status, HttpStatusCode.OK);
		}
	}
}
