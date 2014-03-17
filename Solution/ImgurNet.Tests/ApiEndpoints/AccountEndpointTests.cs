using System;
using System.Net;
using System.Threading.Tasks;
using ImgurNet.ApiEndpoints;
using ImgurNet.Authentication;
using ImgurNet.Exceptions;
using ImgurNet.Models;
using ImgurNet.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImgurNet.Tests.ApiEndpoints
{
	[TestClass]
	public class AccountEndpointTests
	{
		[TestMethod]
		public async Task TestAccountGet()
		{
			var settings = VariousFunctions.LoadTestSettings();

			var imgurClient = new Imgur(new ClientAuthentication(settings.ClientId, false));
			var accountEndpoint = new AccountEndpoint(imgurClient);
			var response = await accountEndpoint.GetAccountDetailsAsync("xerax");

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
			var settings = VariousFunctions.LoadTestSettings();

			var imgurClient = new Imgur(new ClientAuthentication(settings.ClientId, false));
			var accountEndpoint = new AccountEndpoint(imgurClient);
			ImgurResponse<Account> imgurReponse = null;
			try
			{
				imgurReponse = await accountEndpoint.GetAccountDetailsAsync("black-dicks (this account doesn't exist, perfect for le test)");
			}
			catch (ImgurResponseFailedException exception)
			{
				// Assert the Reponse
				Assert.IsNotNull(exception.ImgurResponse.Data);
				Assert.AreEqual(exception.ImgurResponse.Success, false);
				Assert.AreEqual(exception.ImgurResponse.Status, HttpStatusCode.BadRequest);

				// Assert the Data
				Assert.AreEqual(exception.ImgurResponse.Data.ErrorDescription, "A username is required.");
				Assert.AreEqual(exception.ImgurResponse.Data.Method, "GET");
				Assert.AreEqual(exception.ImgurResponse.Data.Request, "/3/account/black-dicks (this account doesn't exist, perfect for le test)");
			}

			Assert.IsNull(imgurReponse);
		}
	}
}
