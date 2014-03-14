using System;
using ImgurNet.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ImgurNet.Tests.Authentication
{
	[TestClass]
	public class ClientAuthenticationTests
	{
		[TestMethod]
		public void TestBasicInfo()
		{
			// Create random ClientId
			var clientId = String.Format("Example-clientid-{0}", new Random().Next(0xbeef, 0xdead));

			// Create a new Client Authentication based off of the ClientId
			var clientAuthentication = new ClientAuthentication(clientId);

			// Validate data is correct
			Assert.IsNotNull(clientAuthentication);
			Assert.AreEqual(clientAuthentication.ClientId, clientId);
			Assert.AreEqual(clientAuthentication.AuthenticationType, AuthenticationType.ClientId);
		}
	}
}
