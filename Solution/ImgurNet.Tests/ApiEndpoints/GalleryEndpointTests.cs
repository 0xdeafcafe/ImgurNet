using System.Net;
using System.Threading.Tasks;
using ImgurNet.ApiEndpoints;
using ImgurNet.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImgurNet.Tests.ApiEndpoints
{
	[TestClass]
	public class GalleryEndpointTests
	{
		[TestMethod]
		public async Task TestGetGalleryImages()
		{
			var imgurClient = AuthenticationHelpers.CreateClientAuthenticatedImgurClient();
			var galleryEndpoint = new GalleryEndpoint(imgurClient);
			var response = await galleryEndpoint.GetGalleryImagesAsync();

			// Assert the Reponse
			Assert.IsNotNull(response.Data);
			Assert.AreEqual(response.Success, true);
			Assert.AreEqual(response.Status, HttpStatusCode.OK);
		}

		[TestMethod]
		public async Task TestSubRedditGalleryImages()
		{
			var imgurClient = AuthenticationHelpers.CreateClientAuthenticatedImgurClient();
			var galleryEndpoint = new GalleryEndpoint(imgurClient);
			var response = await galleryEndpoint.GetSubredditGalleryAsync("gonewild"); // aw yeah

			// Assert the Reponse
			Assert.IsNotNull(response.Data);
			Assert.AreEqual(response.Success, true);
			Assert.AreEqual(response.Status, HttpStatusCode.OK);
		}

		[TestMethod]
		public async Task TestSubRedditGalleryImage()
		{
			var imgurClient = AuthenticationHelpers.CreateClientAuthenticatedImgurClient();
			var galleryEndpoint = new GalleryEndpoint(imgurClient);
			var response = await galleryEndpoint.GetSubredditGalleryImageAsync("gonewild", "tjm2c"); // hot

			// Assert the Reponse
			Assert.IsNotNull(response.Data);
			Assert.AreEqual(response.Success, true);
			Assert.AreEqual(response.Status, HttpStatusCode.OK);
		}

		[TestMethod]
		public async Task TestGallerySearch()
		{
			var imgurClient = AuthenticationHelpers.CreateClientAuthenticatedImgurClient();
			var galleryEndpoint = new GalleryEndpoint(imgurClient);
			var response = await galleryEndpoint.SearchGalleryAsync("monster");

			// Assert the Reponse
			Assert.IsNotNull(response.Data);
			Assert.AreEqual(response.Success, true);
			Assert.AreEqual(response.Status, HttpStatusCode.OK);
		}
	}
}
