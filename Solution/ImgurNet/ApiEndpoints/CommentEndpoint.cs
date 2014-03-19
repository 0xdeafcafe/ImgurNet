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
					Request.SubmitImgurRequestAsync<Comment>(Request.HttpMethod.Post, String.Format(CommentCreateUrl),
						ImgurClient.Authentication, content: multi);
		}
	}
}
