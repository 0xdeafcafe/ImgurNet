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
		public async Task TestGetComment()
		{
			var imgurClient = await AuthenticationHelpers.CreateOAuth2AuthenticatedImgurClient();
			var conversationEndpoint = new ConversationEndpoint(imgurClient);
			var conversations = await conversationEndpoint.GetConversationListAsync();

			// Assert the Reponse
			Assert.IsNotNull(conversations.Data);
			Assert.AreEqual(conversations.Success, true);
			Assert.AreEqual(conversations.Status, HttpStatusCode.OK);
		}
	}
}
