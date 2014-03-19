using System.Net;
using System.Threading.Tasks;
using ImgurNet.ApiEndpoints;
using ImgurNet.Models;
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
			var imgurClient = await AuthenticationHelpers.CreateOAuth2AuthenticatedImgurClient();
			var commentEndpoint = new CommentEndpoint(imgurClient);
			var comment = await commentEndpoint.GetCommentAsync(192351802);

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
			var imgurClient = await AuthenticationHelpers.CreateOAuth2AuthenticatedImgurClient();
			var commentEndpoint = new CommentEndpoint(imgurClient);
			var comment = await commentEndpoint.CreateCommentAsync("test reply", "161n8BB", 193421419);

			// Assert the Reponse
			Assert.IsNotNull(comment.Data);
			Assert.AreEqual(comment.Success, true);
			Assert.AreEqual(comment.Status, HttpStatusCode.OK);
		}

		[TestMethod]
		public async Task TestDeleteComment()
		{
			var imgurClient = await AuthenticationHelpers.CreateOAuth2AuthenticatedImgurClient();
			var commentEndpoint = new CommentEndpoint(imgurClient);
			var comment = await commentEndpoint.CreateCommentAsync("test reply", "161n8BB", 193421419);
			var deleteComment = await commentEndpoint.DeleteCommentAsync(comment.Data.Id);

			// Assert the Reponse
			Assert.IsNotNull(comment.Data);
			Assert.AreEqual(comment.Success, true);
			Assert.AreEqual(comment.Status, HttpStatusCode.OK);

			// Assert the Data
			Assert.AreEqual(deleteComment.Data, true);
		}

		[TestMethod]
		public async Task TestGetCommentReplies()
		{
			var imgurClient = await AuthenticationHelpers.CreateOAuth2AuthenticatedImgurClient();
			var commentEndpoint = new CommentEndpoint(imgurClient);
			var comment = await commentEndpoint.GetCommentRepliesAsync(193421419);

			// Assert the Reponse
			Assert.IsNotNull(comment.Data);
			Assert.AreEqual(comment.Success, true);
			Assert.AreEqual(comment.Status, HttpStatusCode.OK);
		}

		[TestMethod]
		public async Task TestVoteComment()
		{
			var imgurClient = await AuthenticationHelpers.CreateOAuth2AuthenticatedImgurClient();
			var commentEndpoint = new CommentEndpoint(imgurClient);
			var comment = await commentEndpoint.GetCommentAsync(193421419);
			var votedComment = await commentEndpoint.VoteCommentAsync(comment.Data.Id, Enums.Vote.Up);

			// Assert the Reponse
			Assert.IsNotNull(votedComment.Data);
			Assert.AreEqual(votedComment.Success, true);
			Assert.AreEqual(votedComment.Status, HttpStatusCode.OK);
		}

		[TestMethod]
		public async Task TestReportComment()
		{
			var imgurClient = await AuthenticationHelpers.CreateOAuth2AuthenticatedImgurClient();
			var commentEndpoint = new CommentEndpoint(imgurClient);
			var comment = await commentEndpoint.ReportCommentAsync(193420645);

			// Assert the Reponse
			Assert.AreEqual(comment.Success, true);
			Assert.AreEqual(comment.Status, HttpStatusCode.OK);
		}
	}
}
