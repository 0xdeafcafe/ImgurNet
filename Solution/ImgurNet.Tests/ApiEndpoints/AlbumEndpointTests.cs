using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ImgurNet.ApiEndpoints;
using ImgurNet.Authentication;
using ImgurNet.Models;
using ImgurNet.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImgurNet.Tests.ApiEndpoints
{
	[TestClass]
	public class AlbumEndpointTests
	{
		[TestMethod]
		public async Task TestGetAlbumDetails()
		{
			var settings = VariousFunctions.LoadTestSettings();

			var imgurClient = new Imgur(new ClientAuthentication(settings.ClientId, false));
			var albumEndpoint = new AlbumEndpoint(imgurClient);
			var response = await albumEndpoint.GetAlbumDetailsAsync("IPPAY");

			// Assert the Reponse
			Assert.IsNotNull(response.Data);
			Assert.AreEqual(response.Success, true);
			Assert.AreEqual(response.Status, HttpStatusCode.OK);

			// Asset the Data
			Assert.AreEqual(response.Data.Title, "Grassy Snowbound");
			Assert.AreEqual(response.Data.Layout, "blog");
			Assert.AreEqual(response.Data.Privacy, "public");
		}

		[TestMethod]
		public async Task TestGetAllImagesFromAlbum()
		{
			var settings = VariousFunctions.LoadTestSettings();

			var imgurClient = new Imgur(new ClientAuthentication(settings.ClientId, false));
			var albumEndpoint = new AlbumEndpoint(imgurClient);
			var albumDetails = await albumEndpoint.GetAlbumDetailsAsync("IPPAY");
			var albumImages = await albumEndpoint.GetAllImagesFromAlbumAsync(albumDetails.Data.Id);

			// Assert the Reponse
			Assert.IsNotNull(albumImages.Data);
			Assert.AreEqual(albumImages.Success, true);
			Assert.AreEqual(albumImages.Status, HttpStatusCode.OK);

			// Asset the Data
			Assert.AreEqual(albumImages.Data.Length, albumDetails.Data.ImagesCount);
		}

		[TestMethod]
		public async Task TestGetImageFromAlbum()
		{
			var settings = VariousFunctions.LoadTestSettings();
			var authentication = new OAuth2Authentication(settings.ClientId, settings.ClientSecret, false);
			await OAuthHelpers.GetAccessToken(authentication, settings);
			var imgurClient = new Imgur(authentication);
			var albumEndpoint = new AlbumEndpoint(imgurClient);
			var albumImage = await albumEndpoint.GetImageFromAlbumAsync("IPPAY", "66LxpQn");

			// Assert the Reponse
			Assert.IsNotNull(albumImage.Data);
			Assert.AreEqual(albumImage.Success, true);
			Assert.AreEqual(albumImage.Status, HttpStatusCode.OK);

			// Asset the Data
			Assert.AreEqual(albumImage.Data.Id, "66LxpQn");
		}

		[TestMethod]
		public async Task TestCreateAlbum()
		{
			var settings = VariousFunctions.LoadTestSettings();
			var authentication = new OAuth2Authentication(settings.ClientId, settings.ClientSecret, false);
			await OAuthHelpers.GetAccessToken(authentication, settings);
			var imgurClient = new Imgur(authentication);
			var albumEndpoint = new AlbumEndpoint(imgurClient);
			var imageEndpoint = new ImageEndpoint(imgurClient);

			var filePath = VariousFunctions.GetTestsAssetDirectory() + @"\upload-image-example.jpg";
			var imageBinary = File.ReadAllBytes(filePath);

			var title = String.Format("dicks-{0}", new Random().Next(100, 1337));
			var description = String.Format("black dicks, yo-{0}", new Random().Next(100, 1337));

			var uploadedImages = new List<Image>();
			for (var i = 0; i < 2; i++)
				uploadedImages.Add((await imageEndpoint.UploadImageFromBinaryAsync(imageBinary)).Data);
			var createdAlbum = await albumEndpoint.CreateAlbumAsync(uploadedImages, uploadedImages[0], title, description);

			// Assert the Reponse
			Assert.IsNotNull(createdAlbum.Data);
			Assert.AreEqual(createdAlbum.Success, true);
			Assert.AreEqual(createdAlbum.Status, HttpStatusCode.OK);

			// Assert the data
			Assert.AreEqual(createdAlbum.Data.Title, title);
			Assert.AreEqual(createdAlbum.Data.Description, description);
			Assert.AreEqual(createdAlbum.Data.Cover, uploadedImages[0].Id);
			Assert.AreEqual(createdAlbum.Data.ImagesCount, uploadedImages.Count);
			Assert.AreEqual(createdAlbum.Data.Images[0].Id, uploadedImages[0].Id);
		}
	}
}
