namespace ImgurNet.Authentication
{
	public interface IAuthentication
	{
		AuthenticationType AuthenticationType { get; }
	}

	public enum AuthenticationType
	{
		ClientId,
		OAuth
	}
}
