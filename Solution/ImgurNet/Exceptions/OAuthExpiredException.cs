using System;
using ImgurNet.Authentication;

namespace ImgurNet.Exceptions
{
	public class OAuthExpiredException : Exception
	{
		public OAuth2Authentication Authentication { get; private set; }

		public OAuthExpiredException() { }

		public OAuthExpiredException(OAuth2Authentication authentication) { Authentication = authentication; }

		public OAuthExpiredException(OAuth2Authentication authentication, string message)
			: base(message) { Authentication = authentication; }

		public OAuthExpiredException(OAuth2Authentication authentication, string message, Exception innerException)
			: base(message, innerException) { Authentication = authentication; }
	}
}
