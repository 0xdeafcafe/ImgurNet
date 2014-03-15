using System.Net;
using System.Threading.Tasks;
using ImgurNet.ApiEndpoints;
using ImgurNet.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImgurNet.Tests.ApiEndpoints
{
	[TestClass]
	public class AlbumEndpointTests
	{
		[TestMethod]
		public async Task TestGetAlbumDetails()
		{
			var imgurClient = new Imgur(new ClientAuthentication("8db03472c3a6e93", false));
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
			var imgurClient = new Imgur(new ClientAuthentication("8db03472c3a6e93", false));
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
			var imgurClient = new Imgur(new ClientAuthentication("8db03472c3a6e93", false));
			var albumEndpoint = new AlbumEndpoint(imgurClient);
			var albumImage = await albumEndpoint.GetImageFromAlbumAsync("IPPAY", "66LxpQn");

			// Assert the Reponse
			Assert.IsNotNull(albumImage.Data);
			Assert.AreEqual(albumImage.Success, true);
			Assert.AreEqual(albumImage.Status, HttpStatusCode.OK);

			// Asset the Data
			Assert.AreEqual(albumImage.Data.Id, "66LxpQn");
		}
	}
}
