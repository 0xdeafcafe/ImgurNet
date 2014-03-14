using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ImgurNet.Authentication;
using ImgurNet.Exceptions;
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
					try
					{
						var imgurResponse = JsonConvert.DeserializeObject<ImgurResponse<T>>(stringResponse);

						// Validate it
						return ValidateResponse(imgurResponse);
					}
					catch (JsonReaderException ex)
					{
						return null;
					}
				default:
					throw new NotImplementedException("Soon.");
			}
		}

		internal static ImgurResponse<T> ValidateResponse<T>(ImgurResponse<T> imgurResponse)
			where T : DataModelBase
		{
			if (imgurResponse == null) throw new InvalidDataException("The Imgur response is null.");
			if (imgurResponse.Status != HttpStatusCode.OK || !imgurResponse.Success)
				throw new ImgurResponseFailedException<T>(imgurResponse,
					"The response from Imgur was a failure, it has been embedded into the exception to see whats going on.");

			return imgurResponse;
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
