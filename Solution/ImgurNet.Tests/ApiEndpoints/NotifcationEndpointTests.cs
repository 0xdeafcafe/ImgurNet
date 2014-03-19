using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ImgurNet.ApiEndpoints;
using ImgurNet.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImgurNet.Tests.ApiEndpoints
{
	[TestClass]
	public class NotifcationEndpointTests
	{
		[TestMethod]
		public async Task TestGetAccountNotifications()
		{
			var imgurClient = await AuthenticationHelpers.CreateOAuth2AuthenticatedImgurClient();
			var notificationEndpoints = new NotifcationEndpoint(imgurClient);

			var notifications = await notificationEndpoints.GetNotificationsAsync(false);

			// Assert the Reponse
			Assert.IsNotNull(notifications);
			Assert.AreEqual(notifications.Success, true);
			Assert.AreEqual(notifications.Status, HttpStatusCode.OK);
		}

		[TestMethod]
		public async Task TestGetAccountNotification()
		{
			var imgurClient = await AuthenticationHelpers.CreateOAuth2AuthenticatedImgurClient();
			var notificationEndpoints = new NotifcationEndpoint(imgurClient);

			var notifications = await notificationEndpoints.GetNotificationsAsync();
			if (!notifications.Data.Replies.Any()) return;
			var commentNotification = await notificationEndpoints.GetNotificationAsync(notifications.Data.Replies[0].Id);

			// Assert the Comment Replies Response
			Assert.IsNotNull(commentNotification);
			Assert.AreEqual(commentNotification.Success, true);
			Assert.AreEqual(commentNotification.Status, HttpStatusCode.OK);
		}

		[TestMethod]
		public async Task TestSetAccountNotificationViewed()
		{
			var imgurClient = await AuthenticationHelpers.CreateOAuth2AuthenticatedImgurClient();
			var notificationEndpoints = new NotifcationEndpoint(imgurClient);

			var notifications = await notificationEndpoints.GetNotificationsAsync();
			if (!notifications.Data.Replies.Any()) return;
			var commentNotification = await notificationEndpoints.SetNotificationViewedAsync(notifications.Data.Replies[0].Id);

			// Assert the Response
			Assert.IsNotNull(commentNotification);
			Assert.AreEqual(commentNotification.Success, true);
			Assert.AreEqual(commentNotification.Status, HttpStatusCode.OK);

			// Assert the Data
			Assert.AreEqual(commentNotification.Data, true);
		}
	}
}
