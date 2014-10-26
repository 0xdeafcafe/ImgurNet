using ImgurNet.Models;
using ImgurNet.Web;

namespace ImgurNet.Authentication
{
	public class ClientAuthentication : IAuthentication
	{
		/// <summary>
		/// The authenticationType (always ClientId in this class).
		/// </summary>
		public AuthenticationType AuthenticationType { get; private set; }

		/// <summary>
		/// The ClientId of your Imgur application.
		/// </summary>
		public string ClientId { get; private set; }

		/// <summary>
		/// Creates a new Imgur Authorization based off of a client's ClientId.
		/// </summary>
		/// <param name="clientId">The ClientId of your imgur client. Create one here (https://api.imgur.com/oauth2/addclient).</param>
		/// <param name="checkRateLimit">Check, and load into the model, the current ratelimit status of your client.</param>
		public ClientAuthentication(string clientId, bool checkRateLimit)
		{
			ClientId = clientId;
			AuthenticationType = AuthenticationType.ClientId;

			RateLimit = new Credits();
			if (!checkRateLimit) return;
			RateLimit = Request.SubmitImgurRequestAsync<Credits>(Request.HttpMethod.Get, Credits.CreditsUrl, this).Result.Data;
		}
		
		/// <summary>
		/// This holds the current client's rate limit data
		/// </summary>
		public Credits RateLimit { get; internal set; }
	}
}
