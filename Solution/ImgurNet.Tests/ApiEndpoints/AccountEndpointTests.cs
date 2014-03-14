using System;
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
			var response = await accountEndpoint.GetAccountDetails("xerax");

			// Assert the Reponse
			Assert.IsNotNull(response.Data);
			Assert.AreEqual(response.Success, true);
			Assert.AreEqual(response.Status, HttpStatusCode.OK);

			// Assert the Data
			Assert.AreEqual(response.Data.Id, 2662650);
			Assert.AreEqual(response.Data.Created, new DateTime(2012, 12, 16, 04, 12, 1));
		}

		[TestMethod]
		public async Task TestBadAccountGet()
		{
			var imgurClient = new Imgur(new ClientAuthentication("8db03472c3a6e93"));
			var accountEndpoint = new AccountEndpoint(imgurClient);
			ImgurResponse<Account> imgurReponse = null;
			try
			{
				imgurReponse = await accountEndpoint.GetAccountDetails("black-dicks (this account doesn't exist, perfect for le test)");
			}
			catch (ImgurResponseFailedException<Account> exception)
			{
				// Assert the Reponse
				Assert.IsNotNull(exception.ImgurResponse.Data);
				Assert.AreEqual(exception.ImgurResponse.Success, false);
				Assert.AreEqual(exception.ImgurResponse.Status, HttpStatusCode.BadRequest);

				// Assert the Data
				Assert.AreEqual(exception.ImgurResponse.Data.Id, 0);
				Assert.AreEqual(exception.ImgurResponse.Data.Created, new DateTime(1, 1, 1, 0, 0, 0));
				Assert.IsNull(exception.ImgurResponse.Data.Url);
			}

			Assert.IsNull(imgurReponse);
		}
	}
}
