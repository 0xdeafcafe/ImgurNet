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
	public class ConversationEndpoint : BaseEndpoint
	{
		#region Endpoints

		internal const string ConversationsUrl =			"conversations";
		internal const string ConversationUrl =			"conversations/{0}";
		internal const string ConversationReportUrl =	"conversations/report/{0}";
		internal const string ConversationBlockUrl =		"conversations/block/{0}";

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="imgurClient"></param>
		public ConversationEndpoint(Imgur imgurClient)
		{
			ImgurClient = imgurClient;
		}

		/// <summary>
		/// Get list of all conversations for the authenticated in user
		/// </summary>
		public async Task<ImgurResponse<Conversation[]>> GetConversationListAsync()
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (!(ImgurClient.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			return
				await
					Request.SubmitImgurRequestAsync<Conversation[]>(Request.HttpMethod.Get, ConversationsUrl, ImgurClient.Authentication);
		}

		/// <summary>
		/// Get list of all conversations for the authenticated in user
		/// </summary>
		/// <param name="id">The Id of the conversation</param>
		public async Task<ImgurResponse<Conversation>> GetConversationAsync(int id)
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (!(ImgurClient.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			return
				await
					Request.SubmitImgurRequestAsync<Conversation>(Request.HttpMethod.Get, string.Format(ConversationUrl, id), ImgurClient.Authentication);
		}

		/// <summary>
		/// Create a new conversation
		/// </summary>
		/// <param name="recipientUsername">The username of the recipient</param>
		/// <param name="messageBody">The body of the message</param>
		public async Task<ImgurResponse<Boolean>> CreateConversationAsync(string recipientUsername, string messageBody)
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (!(ImgurClient.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			var keyPairs = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("body", messageBody)
			};
			var multi = new FormUrlEncodedContent(keyPairs);

			return
				await
					Request.SubmitImgurRequestAsync<Boolean>(Request.HttpMethod.Post,
						string.Format(ConversationUrl, recipientUsername), ImgurClient.Authentication, content: multi);
		}

		/// <summary>
		/// Delete a conversation
		/// </summary>
		/// <param name="id">The Id of the conversation</param>
		public async Task<ImgurResponse<Boolean>> DeleteConversationAsync(int id)
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (!(ImgurClient.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			return
				await
					Request.SubmitImgurRequestAsync<Boolean>(Request.HttpMethod.Delete, string.Format(ConversationUrl, id), ImgurClient.Authentication);
		}

		/// <summary>
		/// Report a user for sending messages that are against the Terms of Service.
		/// </summary>
		/// <param name="username">The username of the sender</param>
		public async Task<ImgurResponse<Boolean>> ReportConversationSenderAsync(string username)
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (!(ImgurClient.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			return
				await
					Request.SubmitImgurRequestAsync<Boolean>(Request.HttpMethod.Post, string.Format(ConversationReportUrl, username), ImgurClient.Authentication);
		}

		/// <summary>
		/// Block the user from sending messages to the user that is logged in.
		/// </summary>
		/// <param name="username">The username of the person to block</param>
		public async Task<ImgurResponse<Boolean>> BlockConversationSenderAsync(string username)
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (!(ImgurClient.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			return
				await
					Request.SubmitImgurRequestAsync<Boolean>(Request.HttpMethod.Post, string.Format(ConversationBlockUrl, username), ImgurClient.Authentication);
		}
	}
}
