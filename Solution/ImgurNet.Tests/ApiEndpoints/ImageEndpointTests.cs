using System;
using System.Net;
using System.Threading.Tasks;
using ImgurNet.ApiEndpoints;
using ImgurNet.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImgurNet.Tests.ApiEndpoints
{
	[TestClass]
	public class ImageEndpointTests
	{
		[TestMethod]
		public async Task TestAccountGet()
		{
			var imgurClient = new Imgur(new ClientAuthentication("8db03472c3a6e93"));
			var imageEndpoint = new ImageEndpoint(imgurClient);
			var response = await imageEndpoint.GetImageDetails("F1sUnHq");

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
			Assert.AreEqual(response.Data.AddedToGallery, new DateTime(2014, 2, 21, 11, 41, 46));
		}
	}
}
