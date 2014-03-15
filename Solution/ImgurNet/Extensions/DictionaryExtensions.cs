using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace ImgurNet.Extensions
{
	internal static class DictionaryExtensions
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dictionary"></param>
		/// <param name="uri"></param>
		/// <returns></returns>
		internal static Uri ToQueryString(this Dictionary<string, string> dictionary, Uri uri)
		{
			return new Uri(dictionary.ToQueryString(uri.ToString()), UriKind.Absolute);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dictionary"></param>
		/// <param name="url"></param>
		/// <returns></returns>
		internal static string ToQueryString(this Dictionary<string, string> dictionary, string url)
		{
			if (dictionary.Count == 0) return url;

			var array = dictionary.Select(entry => string.Format("{0}={1}", Uri.EscapeUriString(entry.Key), Uri.EscapeUriString(entry.Value))).ToList();
			return String.Format("{0}?{1}", url, String.Join("&", array));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dictionary"></param>
		/// <param name="url"></param>
		/// <returns></returns>
		internal static string ToStringCollection(this Dictionary<string, string> dictionary)
		{
			var array = dictionary.Select(entry => string.Format("{0}={1}", WebUtility.UrlEncode(entry.Key), WebUtility.UrlEncode(entry.Value))).ToList();
			return String.Format("{0}", String.Join("&", array));
		}
	}
}
