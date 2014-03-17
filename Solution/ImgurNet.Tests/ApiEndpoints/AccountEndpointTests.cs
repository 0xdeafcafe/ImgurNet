using System;
using System.IO;
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
		public async Task TestGetAccount()
		{
			var settings = VariousFunctions.LoadTestSettings();

			var imgurClient = new Imgur(new ClientAuthentication(settings.ClientId, false));
			var accountEndpoint = new AccountEndpoint(imgurClient);
			var response = await accountEndpoint.GetAccountDetailsAsync("xerax");

			// Assert the Response
			Assert.IsNotNull(response.Data);
			Assert.AreEqual(response.Success, true);
			Assert.AreEqual(response.Status, HttpStatusCode.OK);

			// Assert the Data
			Assert.AreEqual(response.Data.Id, 2662650);
			Assert.AreEqual(response.Data.Created, new DateTime(2012, 12, 16, 04, 12, 1));
		}

		[TestMethod]
		public async Task TestGetBadAccount()
		{
			var settings = VariousFunctions.LoadTestSettings();

			var imgurClient = new Imgur(new ClientAuthentication(settings.ClientId, false));
			var accountEndpoint = new AccountEndpoint(imgurClient);
			ImgurResponse<Account> imgurResponse = null;
			try
			{
				imgurResponse = await accountEndpoint.GetAccountDetailsAsync("black-dicks (this account doesn't exist, perfect for le test)");
			}
			catch (ImgurResponseFailedException exception)
			{
				// Assert the Response
				Assert.IsNotNull(exception.ImgurResponse.Data);
				Assert.AreEqual(exception.ImgurResponse.Success, false);
				Assert.AreEqual(exception.ImgurResponse.Status, HttpStatusCode.BadRequest);

				// Assert the Data
				Assert.AreEqual(exception.ImgurResponse.Data.ErrorDescription, "A username is required.");
				Assert.AreEqual(exception.ImgurResponse.Data.Method, "GET");
				Assert.AreEqual(exception.ImgurResponse.Data.Request, "/3/account/black-dicks (this account doesn't exist, perfect for le test)");
			}

			Assert.IsNull(imgurResponse);
		}

		[TestMethod]
		public async Task TestGetAccountImageDetails()
		{
			var settings = VariousFunctions.LoadTestSettings();
			var authentication = new OAuth2Authentication(settings.ClientId, settings.ClientSecret, false);
			await OAuthHelpers.GetAccessToken(authentication, settings);
			var imgurClient = new Imgur(authentication);
			var accountEndpoint = new AccountEndpoint(imgurClient);
			var accountImageCount = await accountEndpoint.GetAccountImageIds();
			if (accountImageCount.Data.Length == 0) return;
			var accountImageDetails = await accountEndpoint.GetAccountImageDetails(accountImageCount.Data[0]);

			// Assert the Response
			Assert.IsNotNull(accountImageDetails.Data);
			Assert.AreEqual(accountImageDetails.Success, true);
			Assert.AreEqual(accountImageDetails.Status, HttpStatusCode.OK);
		}

		[TestMethod]
		public async Task TestGetAccountImageIds()
		{
			var settings = VariousFunctions.LoadTestSettings();
			var authentication = new OAuth2Authentication(settings.ClientId, settings.ClientSecret, false);
			await OAuthHelpers.GetAccessToken(authentication, settings);
			var imgurClient = new Imgur(authentication);
			var accountEndpoint = new AccountEndpoint(imgurClient);
			var accountImageCount = await accountEndpoint.GetAccountImageIds();

			// Assert the Response
			Assert.IsNotNull(accountImageCount.Data);
			Assert.AreEqual(accountImageCount.Success, true);
			Assert.AreEqual(accountImageCount.Status, HttpStatusCode.OK);
		}

		[TestMethod]
		public async Task TestGetAccountImageCount()
		{
			var settings = VariousFunctions.LoadTestSettings();
			var authentication = new OAuth2Authentication(settings.ClientId, settings.ClientSecret, false);
			await OAuthHelpers.GetAccessToken(authentication, settings);
			var imgurClient = new Imgur(authentication);
			var accountEndpoint = new AccountEndpoint(imgurClient);
			var accountImageCount = await accountEndpoint.GetAccountImageCount();

			// Assert the Response
			Assert.IsNotNull(accountImageCount.Data);
			Assert.AreEqual(accountImageCount.Success, true);
			Assert.AreEqual(accountImageCount.Status, HttpStatusCode.OK);
		}

		[TestMethod]
		public async Task TestDeleteAccountImage()
		{
			var settings = VariousFunctions.LoadTestSettings();
			var authentication = new OAuth2Authentication(settings.ClientId, settings.ClientSecret, false);
			await OAuthHelpers.GetAccessToken(authentication, settings);
			var imgurClient = new Imgur(authentication);
			var accountEndpoint = new AccountEndpoint(imgurClient);
			var imageEndpoint = new ImageEndpoint(imgurClient);

			// Upload Image
			var filePath = VariousFunctions.GetTestsAssetDirectory() + @"\upload-image-example.jpg";
			var imageBinary = File.ReadAllBytes(filePath);
			var image = await imageEndpoint.UploadImageFromBinaryAsync(imageBinary);

			// Delete Image
			var deletedImage = await accountEndpoint.DeleteAccountImage(image.Data.DeleteHash);

			// Assert the Response
			Assert.IsNotNull(deletedImage.Data);
			Assert.AreEqual(deletedImage.Success, true);
			Assert.AreEqual(deletedImage.Status, HttpStatusCode.OK);
			Assert.AreEqual(deletedImage.Data, true);
		}
	}
}
