using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using ImgurNet.Authentication;
using ImgurNet.Models;
using Newtonsoft.Json;

namespace ImgurNet.Web
{
	internal static class Request
	{
		internal static readonly string ImgurApiV3Base = "https://api.imgur.com/3/{0}";

		internal static async Task<ImgurResponse<T>> Get<T>(string endpoint, IAuthentication authentication)
			where T : DataModelBase
		{
			return await DoWebRequest<T>(HttpMethod.Get, new Uri(String.Format(ImgurApiV3Base, endpoint)), authentication);
		}

		private async static Task<ImgurResponse<T>> DoWebRequest<T>(HttpMethod httpMethod, Uri endpointUri, IAuthentication authentication)
			where T : DataModelBase
		{
			var httpClient = new HttpClient();
			switch (authentication.AuthenticationType)
			{
				case AuthenticationType.ClientId:
					var clientAuthentication = authentication as ClientAuthentication;
					if (clientAuthentication == null) 
						throw new InvalidDataException(
							"This should not have happened. The authentication interface is not of type ClientAuthentication, yet it's type says it is. PANIC. (nah, just tweet @alexerax).");

					httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization",
						String.Format("Client-ID {0}", clientAuthentication.ClientId));
					break;

				case AuthenticationType.OAuth:
					throw new NotImplementedException("ImgurNet doesn't support OAuth currently. Soon!");
			}

			switch (httpMethod)
			{
				case HttpMethod.Get:
					var httpResponse = await httpClient.GetAsync(endpointUri);
					var stringResponse = await httpResponse.Content.ReadAsStringAsync();
					var output = JsonConvert.DeserializeObject<ImgurResponse<T>>(stringResponse);
					return output;

				default:
					throw new NotImplementedException("Soon.");
			}
		}

		internal enum HttpMethod
		{
			Get,
			Post,
			Put,
			Delete
		}
	}
}
