using System.Net;
using System.Threading.Tasks;
using ImgurNet.ApiEndpoints;
using ImgurNet.Authentication;
using ImgurNet.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImgurNet.Tests.ApiEndpoints
{
	[TestClass]
	public class CommentEndpointTests
	{
		[TestMethod]
		public async Task TestGetComment()
		{
			var settings = VariousFunctions.LoadTestSettings();
			var authentication = new OAuth2Authentication(settings.ClientId, settings.ClientSecret, false);
			await OAuthHelpers.GetAccessToken(authentication, settings);
			var imgurClient = new Imgur(authentication);
			var commentEndpoint = new CommentEndpoint(imgurClient);
			var comment = await commentEndpoint.GetComment(192351802);

			// Assert the Reponse
			Assert.IsNotNull(comment.Data);
			Assert.AreEqual(comment.Success, true);
			Assert.AreEqual(comment.Status, HttpStatusCode.OK);

			// Assert the Data
			Assert.AreEqual(comment.Data.OnAlbum, false);
			Assert.AreEqual(comment.Data.AlbumCover, null);
			Assert.AreEqual(comment.Data.Author, "imgurnet");
		}

		[TestMethod]
		public async Task TestCreateComment()
		{
			var settings = VariousFunctions.LoadTestSettings();
			var authentication = new OAuth2Authentication(settings.ClientId, settings.ClientSecret, false);
			await OAuthHelpers.GetAccessToken(authentication, settings);
			var imgurClient = new Imgur(authentication);
			var commentEndpoint = new CommentEndpoint(imgurClient);
			var comment = await commentEndpoint.CreateComment("test reply", "161n8BB", 193421419);

			// Assert the Reponse
			Assert.IsNotNull(comment.Data);
			Assert.AreEqual(comment.Success, true);
			Assert.AreEqual(comment.Status, HttpStatusCode.OK);

			// Assert the Data
			Assert.AreEqual(comment.Data.Caption, "test reply");
		}
	}
}
