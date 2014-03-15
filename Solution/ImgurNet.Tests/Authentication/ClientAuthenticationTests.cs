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
			var clientAuthentication = new ClientAuthentication(clientId, false);

			// Validate data is correct
			Assert.IsNotNull(clientAuthentication);
			Assert.AreEqual(clientAuthentication.ClientId, clientId);
			Assert.AreEqual(clientAuthentication.AuthenticationType, AuthenticationType.ClientId);
		}

		[TestMethod]
		public void TestRateLimitInitalization()
		{
			// Create a new Client Authentication based off of the ClientId
			var clientAuthentication = new ClientAuthentication("8db03472c3a6e93", true);
			
			// Validate rate limit data is correct
			Assert.IsTrue(clientAuthentication.RateLimit.ClientLimit > 0);
			Assert.IsTrue(clientAuthentication.RateLimit.UserLimit > 0);
		}
	}
}
