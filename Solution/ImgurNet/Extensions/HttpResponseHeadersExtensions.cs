using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace ImgurNet.Extensions
{
	internal static class HttpResponseHeadersExtensions
	{
		/// <summary>
		/// Gets a method to get a header from a header collection. Why this isn't included out of the box is beyond me.
		/// </summary>
		/// <param name="headers">The Http Response Headers collection.</param>
		/// <param name="target">The target header name to get.</param>
		/// <returns>The header data!</returns>
		internal static string GetValue(this HttpResponseHeaders headers, string target)
		{
			IEnumerable<string> headerValueList;
			headers.TryGetValues(target, out headerValueList);
			if (headerValueList == null) return null;
			var headerValueListSafe = headerValueList as IList<string> ?? headerValueList.ToList();
			return !headerValueListSafe.Any() ? null : headerValueListSafe[0];
		}
	}
}
