namespace ImgurNet.Authentication
{
	public class ClientAuthentication : IAuthentication
	{
		/// <summary>
		/// The authenticationType (always ClientId in this class)
		/// </summary>
		public AuthenticationType AuthenticationType { get; private set; }

		/// <summary>
		/// The ClientId of your imgur application
		/// </summary>
		public string ClientId { get; private set; }

		/// <summary>
		/// Creates a new Imgur Authorization based off of a client's ClientId
		/// </summary>
		/// <param name="clientId">The ClientId of your imgur client. Create one here (https://api.imgur.com/oauth2/addclient)</param>
		public ClientAuthentication(string clientId)
		{
			ClientId = clientId;
			AuthenticationType = AuthenticationType.ClientId;
		}
	}
}
