using System;
using System.Net;
using System.Threading.Tasks;
using ImgurNet.ApiEndpoints;
using ImgurNet.Exceptions;
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

		[TestMethod]
		public async Task TestCreateConversation()
		{
			var imgurClient = await AuthenticationHelpers.CreateOAuth2AuthenticatedImgurClient();
			var conversationEndpoint = new ConversationEndpoint(imgurClient);
			var conversation = await conversationEndpoint.CreateConversationAsync("xerax", "bitchin unit tests!");

			// Assert the Reponse
			Assert.IsNotNull(conversation.Data);
			Assert.AreEqual(conversation.Success, true);
			Assert.AreEqual(conversation.Status, HttpStatusCode.OK);
			Assert.AreEqual(conversation.Data, true);
		}

		[TestMethod]
		public async Task TestDeleteConversation()
		{
			var imgurClient = await AuthenticationHelpers.CreateOAuth2AuthenticatedImgurClient();
			var conversationEndpoint = new ConversationEndpoint(imgurClient);
			await conversationEndpoint.CreateConversationAsync("xerax", "bitchin unit tests!");
			var conversations = await conversationEndpoint.GetConversationListAsync();
			if (conversations.Data.Length <= 0) return;
			var deletedConversation = await conversationEndpoint.DeleteConversationAsync(conversations.Data[0].Id);

			// Assert the Reponse
			Assert.IsNotNull(deletedConversation.Data);
			Assert.AreEqual(deletedConversation.Success, true);
			Assert.AreEqual(deletedConversation.Status, HttpStatusCode.OK);
			Assert.AreEqual(deletedConversation.Data, true);
		}

		[TestMethod]
		public async Task TestReportuserFromConversation()
		{
			var imgurClient = await AuthenticationHelpers.CreateOAuth2AuthenticatedImgurClient();
			var conversationEndpoint = new ConversationEndpoint(imgurClient);
			try
			{
				await conversationEndpoint.ReportConversationSenderAsync(String.Format("test-username-{0}", new Random().Next(0, 1000)));

				Assert.Fail();
			}
			catch (ImgurResponseFailedException exception)
			{
				// Assert the Reponse
				Assert.AreEqual(exception.Message, "Invalid username");
			}
		}

		[TestMethod]
		public async Task TestBlockUserFromConversation()
		{
			var imgurClient = await AuthenticationHelpers.CreateOAuth2AuthenticatedImgurClient();
			var conversationEndpoint = new ConversationEndpoint(imgurClient);
			try
			{
				await conversationEndpoint.BlockConversationSenderAsync(String.Format("test-username-{0}", new Random().Next(0, 1000)));

				Assert.Fail();
			}
			catch (ImgurResponseFailedException exception)
			{
				// Assert the Reponse
				Assert.AreEqual(exception.Message, "Invalid username");
			}
		}
	}
}
