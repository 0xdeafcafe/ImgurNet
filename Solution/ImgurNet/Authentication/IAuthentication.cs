using ImgurNet.Models;

namespace ImgurNet.Authentication
{
	public interface IAuthentication
	{
		/// <summary>
		/// The type of authentication that the interface is being implimented into
		/// </summary>
		AuthenticationType AuthenticationType { get; }

		Credits RateLimit { get; }
	}

	public enum AuthenticationType
	{
		ClientId,
		OAuth2
	}
}
