using System.Net;
using System.Threading.Tasks;
using ImgurNet.ApiEndpoints;
using ImgurNet.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImgurNet.Tests.ApiEndpoints
{
	[TestClass]
	public class ConversationEndpointTests
	{
		[TestMethod]
		public async Task TestGetConversationList()
		{
			var imgurClient = await AuthenticationHelpers.CreateOAuth2AuthenticatedImgurClient();
			var conversationEndpoint = new ConversationEndpoint(imgurClient);
			var conversations = await conversationEndpoint.GetConversationListAsync();

			// Assert the Reponse
			Assert.IsNotNull(conversations.Data);
			Assert.AreEqual(conversations.Success, true);
			Assert.AreEqual(conversations.Status, HttpStatusCode.OK);
		}

		[TestMethod]
		public async Task TestGetConversation()
		{
			var imgurClient = await AuthenticationHelpers.CreateOAuth2AuthenticatedImgurClient();
			var conversationEndpoint = new ConversationEndpoint(imgurClient);
			var conversations = await conversationEndpoint.GetConversationListAsync();
			if (conversations.Data.Length <= 0) return;
			var conversation = await conversationEndpoint.GetConversationAsync(conversations.Data[0].Id);

			// Assert the Reponse
			Assert.IsNotNull(conversation.Data);
			Assert.AreEqual(conversation.Success, true);
			Assert.AreEqual(conversation.Status, HttpStatusCode.OK);
		}
	}
}
