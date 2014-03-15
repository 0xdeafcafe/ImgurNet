using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ImgurNet.ApiEndpoints;
using ImgurNet.Authentication;
using ImgurNet.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImgurNet.Tests.ApiEndpoints
{
	[TestClass]
	public class ImageEndpointTests
	{
		[TestMethod]
		public async Task TestGetImageDetails()
		{
			var imgurClient = new Imgur(new ClientAuthentication("8db03472c3a6e93", false));
			var imageEndpoint = new ImageEndpoint(imgurClient);
			var response = await imageEndpoint.GetImageDetailsAsync("F1sUnHq");

			// Assert the Reponse
			Assert.IsNotNull(response.Data);
			Assert.AreEqual(response.Success, true);
			Assert.AreEqual(response.Status, HttpStatusCode.OK);

			// Asset the Data
			Assert.AreEqual(response.Data.Type, "image/gif");
			Assert.AreEqual(response.Data.Id, "F1sUnHq");
			Assert.AreEqual(response.Data.Height, 172);
			Assert.AreEqual(response.Data.Width, 230);
			Assert.AreEqual(response.Data.Animated, true);
			Assert.AreEqual(response.Data.Link, "http://i.imgur.com/F1sUnHq.gif");
			Assert.AreEqual(response.Data.AddedToGallery, new DateTime(2014, 2, 21, 23, 41, 46));
		}

		[TestMethod]
		public async Task TestDeleteImage()
		{
			var filePath = AppDomain.CurrentDomain.BaseDirectory + @"\Assets\upload-image-example.jpg";
			var imageBinary = File.ReadAllBytes(filePath);

			var imgurClient = new Imgur(new ClientAuthentication("8db03472c3a6e93", false));
			var imageEndpoint = new ImageEndpoint(imgurClient);
			var uploadedImage = await imageEndpoint.UploadImageFromBinaryAsync(imageBinary);
			var response = await imageEndpoint.DeleteImageAsync(uploadedImage.Data.DeleteHash);

			// Assert the Reponse
			Assert.IsNotNull(response.Data);
			Assert.AreEqual(response.Success, true);
			Assert.AreEqual(response.Status, HttpStatusCode.OK);
			Assert.IsTrue(response.Data);
		}

		[TestMethod]
		public async Task TestImageUploadFromBinary()
		{
			var filePath = AppDomain.CurrentDomain.BaseDirectory + @"\Assets\upload-image-example.jpg";
			var imageBinary = File.ReadAllBytes(filePath);

			var imgurClient = new Imgur(new ClientAuthentication("8db03472c3a6e93", false));
			var imageEndpoint = new ImageEndpoint(imgurClient);

			try
			{
				var response = await imageEndpoint.UploadImageFromBinaryAsync(imageBinary, title: "yolo", description: "Keep Calm, because yolo #420");

				// Assert the Reponse
				Assert.IsNotNull(response.Data);
				Assert.AreEqual(response.Success, true);
				Assert.AreEqual(response.Status, HttpStatusCode.OK);
			}
			catch (ImgurResponseFailedException exception)
			{
				Assert.Fail(exception.ImgurResponse.Data.ErrorDescription);
			}
		}

		[TestMethod]
		public async Task TestImageUploadFromUrl()
		{
			const string imageUrl = "http://www.ella-lapetiteanglaise.com/wp-content/uploads/2013/11/keep-calm-because-yolo-24.png";

			var imgurClient = new Imgur(new ClientAuthentication("8db03472c3a6e93", false));
			var imageEndpoint = new ImageEndpoint(imgurClient);

			try
			{
				var response = await imageEndpoint.UploadImageFromUrlAsync(imageUrl, title: "yolo", description: "Keep Calm, because yolo #420");

				// Assert the Reponse
				Assert.IsNotNull(response.Data);
				Assert.AreEqual(response.Success, true);
				Assert.AreEqual(response.Status, HttpStatusCode.OK);
			}
			catch (ImgurResponseFailedException exception)
			{
				Assert.Fail(exception.ImgurResponse.Data.ErrorDescription);
			}
		}
	}
}
