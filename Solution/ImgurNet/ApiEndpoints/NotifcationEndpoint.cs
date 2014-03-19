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
	public class NotifcationEndpoint : BaseEndpoint
	{
		#region Endpoints

		internal string NotificationsUrl = "notification";
		internal string NotificationUrl = "notification/{0}";

		#endregion
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="imgur"></param>
		public NotifcationEndpoint(Imgur imgur)
		{
			ImgurClient = imgur;
		}

		/// <summary>
		/// Get a list of all notifications from the authorized account
		/// </summary>
		/// <param name="onlyShowNew">Only retrieve new notifications (un-viewed)</param>
		public async Task<ImgurResponse<NotificationCenter>> GetNotificationsAsync(bool onlyShowNew = true)
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (!(ImgurClient.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			var keyPairs = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("new", onlyShowNew.ToString().ToLowerInvariant())
			};
			var multi = new FormUrlEncodedContent(keyPairs.ToArray());

			return await Request.SubmitImgurRequestAsync<NotificationCenter>(Request.HttpMethod.Get, NotificationsUrl, ImgurClient.Authentication, content: multi);
		}

		/// <summary>
		/// Get a specific notification
		/// </summary>
		/// <param name="notificationId">The notification to get data for</param>
		public async Task<ImgurResponse<Notification<object>>> GetNotificationAsync(string notificationId)
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (!(ImgurClient.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			return await Request.SubmitImgurRequestAsync<Notification<object>>(Request.HttpMethod.Get, string.Format(NotificationUrl, notificationId), ImgurClient.Authentication);
		}

		/// <summary>
		/// Marks a notification as viewed, this way it no longer shows up in the basic notification request
		/// </summary>
		/// <param name="notificationId">The notification to mark as viewed</param>
		/// <returns>A boolean indicating if the transaction was successful</returns>
		public async Task<ImgurResponse<Boolean>> SetNotificationViewedAsync(string notificationId)
		{
			if (ImgurClient.Authentication == null)
				throw new InvalidAuthenticationException("Authentication can not be null. Set it in the main Imgur class.");

			if (!(ImgurClient.Authentication is OAuth2Authentication))
				throw new InvalidAuthenticationException("You need to use OAuth2Authentication to call this Endpoint.");

			return await Request.SubmitImgurRequestAsync<Boolean>(Request.HttpMethod.Put, string.Format(NotificationUrl, notificationId), ImgurClient.Authentication);
		}
	}
}
