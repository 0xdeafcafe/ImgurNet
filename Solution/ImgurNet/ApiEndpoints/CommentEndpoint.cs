using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ImgurNet.Authentication;
using ImgurNet.Exceptions;
using ImgurNet.Models;
using ImgurNet.Web;

namespace ImgurNet.ApiEndpoints
{
	public class CommentEndpoint : BaseEndpoint
	{
		#region Endpoints

		internal const string CommentCreateUrl =		"comment/";
		internal const string CommentDetailsUrl =		"comment/{0}";
		internal const string CommentDeleteUrl =		"comment/{0}";
		internal const string CommentReportUrl =		"comment/{0}/report";
		internal const string CommentRepliesUrl =		"comment/{0}/replies";
		internal const string CommentVoteUrl =			"comment/{0}/vote/{1}";

		#endregion

		public CommentEndpoint(Imgur imgurClient)
		{
			ImgurClient = imgurClient;
		}

		/// <summary>
		/// Get information about a specific comment
		/// </summary>
		/// <param name="commentId">The Id of the comment</param>
		public async Task<ImgurResponse<Comment>> GetCommentAsync(Int64 commentId)
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (!(ImgurClient.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			return
				await
					Request.SubmitImgurRequestAsync<Comment>(Request.HttpMethod.Get, String.Format(CommentDetailsUrl, commentId),
						ImgurClient.Authentication);
		}

		/// <summary>
		/// Creates a new comment
		/// </summary>
		/// <param name="caption">The body of the comment</param>
		/// <param name="imageId">The image to post the comment on</param>
		/// <param name="parentId">[optional] The id of the comment this is a reply to, (if this is a reply)</param>
		public async Task<ImgurResponse<Comment>> CreateCommentAsync(string caption, string imageId, int? parentId = null)
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (!(ImgurClient.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			var keyPairs = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("image_id", imageId),
				new KeyValuePair<string, string>("comment", caption)
			};
			if (parentId != null) keyPairs.Add(new KeyValuePair<string, string>("parent_id", parentId.ToString()));
			var multi = new FormUrlEncodedContent(keyPairs.ToArray());

			return
				await
					Request.SubmitImgurRequestAsync<Comment>(Request.HttpMethod.Post, CommentCreateUrl,
						ImgurClient.Authentication, content: multi);
		}

		/// <summary>
		/// Delete a comment by the given id
		/// </summary>
		/// <param name="commentId">The Id of the comment</param>
		public async Task<ImgurResponse<Boolean>> DeleteCommentAsync(Int64 commentId)
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (!(ImgurClient.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			return
				await
					Request.SubmitImgurRequestAsync<Boolean>(Request.HttpMethod.Delete, String.Format(CommentDeleteUrl, commentId),
						ImgurClient.Authentication);
		}

		/// <summary>
		/// Get a comment with all of the replies for that comment
		/// </summary>
		/// <param name="commentId">The Id of the comment</param>
		public async Task<ImgurResponse<Comment>> GetCommentRepliesAsync(Int64 commentId)
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (!(ImgurClient.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			return
				await
					Request.SubmitImgurRequestAsync<Comment>(Request.HttpMethod.Get, String.Format(CommentRepliesUrl, commentId),
						ImgurClient.Authentication);
		}

		/// <summary>
		/// Vote on a comment
		/// </summary>
		/// <param name="commentId">The Id of the comment</param>
		/// <param name="vote">The vote to give the comment</param>
		public async Task<ImgurResponse<Boolean>> VoteCommentAsync(Int64 commentId, VoteDirection vote)
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (!(ImgurClient.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			return
				await
					Request.SubmitImgurRequestAsync<Boolean>(Request.HttpMethod.Get, String.Format(CommentVoteUrl, commentId, vote.ToString().ToLowerInvariant()),
						ImgurClient.Authentication);
		}

		/// <summary>
		/// Report a comment (made by a loser/douchebag) for being inappropriate
		/// </summary>
		/// <param name="commentId">The Id of the comment</param>
		public async Task<ImgurResponse<object>> ReportCommentAsync(Int64 commentId)
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (!(ImgurClient.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			return
				await
					Request.SubmitImgurRequestAsync<object>(Request.HttpMethod.Post, String.Format(CommentReportUrl, commentId),
						ImgurClient.Authentication);
		}
	}
}
