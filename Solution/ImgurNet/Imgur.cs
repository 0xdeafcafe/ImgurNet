using System;
using ImgurNet.Authentication;

namespace ImgurNet
{
	public class Imgur
	{
		/// <summary>
		/// The authentication that this Client uses
		/// </summary>
		public IAuthentication Authentication { get; private set; }
		
		/// <summary>
		/// Creates a new instance of the Imgur Client, that can be passed into Endpoints
		/// </summary>
		/// <param name="authentication">Your authentication.</param>
		public Imgur(IAuthentication authentication)
		{
			Authentication = authentication;
		}

		/// <summary>
		/// Updates the authentication that this Client uses.
		/// </summary>
		/// <param name="authentication">The authentication to put into this Client.</param>
		public void ChangeAuthentication(IAuthentication authentication)
		{
			if (authentication.AuthenticationType == AuthenticationType.OAuth)
				throw new NotImplementedException();

			Authentication = authentication;
		}
	}
}
