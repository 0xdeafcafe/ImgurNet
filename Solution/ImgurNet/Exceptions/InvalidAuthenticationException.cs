using System;

namespace ImgurNet.Exceptions
{
	public class InvalidAuthenticationException : Exception
	{
		public InvalidAuthenticationException(string message)
			: base(message)
		{
			
		}

		public InvalidAuthenticationException(string message, Exception innerException)
			: base(message, innerException)
		{

		}
	}
}
