using System;
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

		internal const string CommentUrl =			"comment/{0}";

		#endregion

		public CommentEndpoint(Imgur imgurClient)
		{
			ImgurClient = imgurClient;
		}

		/// <summary>
		/// Get information about a specific comment
		/// </summary>
		/// <param name="commentId">The Id of the comment</param>
		public async Task<ImgurResponse<Comment>> GetComment(Int64 commentId)
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (!(ImgurClient.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			return
				await
					Request.SubmitImgurRequestAsync<Comment>(Request.HttpMethod.Get, String.Format(CommentUrl, commentId),
						ImgurClient.Authentication);
		}
	}
}
