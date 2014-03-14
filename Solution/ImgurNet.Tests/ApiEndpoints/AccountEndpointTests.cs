using System.Threading.Tasks;
using ImgurNet.ApiEndpoints;
using ImgurNet.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImgurNet.Tests.ApiEndpoints
{
	[TestClass]
	public class AccountEndpointTests
	{
		[TestMethod]
		public async Task TestAccountGet()
		{
			var imgurClient = new Imgur(new ClientAuthentication("8db03472c3a6e93"));
			var accountEndpoint = new AccountEndpoint(imgurClient);
			var response = await accountEndpoint.GetAccount("xerax");

			Assert.IsNotNull(response.Data);
			Assert.AreEqual(response.Success, true);
			Assert.AreEqual(response.Status, 200);
			Assert.AreEqual(response.Data.Id, 2662650);
			Assert.AreEqual(response.Data.Created, 1355631121);
		}

		[TestMethod]
		public async Task TestBadAccountGet()
		{
			var imgurClient = new Imgur(new ClientAuthentication("8db03472c3a6e93"));
			var accountEndpoint = new AccountEndpoint(imgurClient);
			var response = await accountEndpoint.GetAccount("black-dick (this account doesn't exist, perfect for le test)");

			Assert.IsNotNull(response.Data);
			Assert.AreEqual(response.Success, false);
			Assert.AreEqual(response.Status, 400);
			Assert.AreEqual(response.Data.Id, 0);
			Assert.AreEqual(response.Data.Created, 0);
			Assert.IsNull(response.Data.Url);
		}
	}
}
