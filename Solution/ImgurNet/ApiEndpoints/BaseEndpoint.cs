namespace ImgurNet.ApiEndpoints
{
	public class BaseEndpoint : IEndpoint
	{
		public Imgur ImgurClient { get; internal set; }
	}
}
