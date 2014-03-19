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

		internal const string ConversationsUrl =		"conversations";
		internal const string ConversationUrl =			"conversations/{0}";

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
	}
}
