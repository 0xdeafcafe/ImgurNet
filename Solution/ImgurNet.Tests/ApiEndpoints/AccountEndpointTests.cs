using System.Net;
using System.Threading.Tasks;
using ImgurNet.ApiEndpoints;
using ImgurNet.Authentication;
using ImgurNet.Exceptions;
using ImgurNet.Models;
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
			Assert.AreEqual(response.Status, HttpStatusCode.OK);
			Assert.AreEqual(response.Data.Id, 2662650);
			Assert.AreEqual(response.Data.Created, 1355631121);
		}

		[TestMethod]
		public async Task TestBadAccountGet()
		{
			var imgurClient = new Imgur(new ClientAuthentication("8db03472c3a6e93"));
			var accountEndpoint = new AccountEndpoint(imgurClient);
			ImgurResponse<Account> imgurReponse = null;
			try
			{
				imgurReponse = await accountEndpoint.GetAccount("black-dicks (this account doesn't exist, perfect for le test)");
			}
			catch (ImgurResponseFailedException<Account> exception)
			{
				Assert.IsNotNull(exception.ImgurResponse.Data);
				Assert.AreEqual(exception.ImgurResponse.Success, false);
				Assert.AreEqual(exception.ImgurResponse.Status, HttpStatusCode.BadRequest);
				Assert.AreEqual(exception.ImgurResponse.Data.Id, 0);
				Assert.AreEqual(exception.ImgurResponse.Data.Created, 0);
				Assert.IsNull(exception.ImgurResponse.Data.Url);
			}

			Assert.IsNull(imgurReponse);
		}
	}
}
