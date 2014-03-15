using System;

namespace ImgurNet.Converters.Generic
{
	internal static class UnixTimestampConverters
	{
		/// <summary>
		/// Covnerts a Double representing a Unix Timestamp to a DateTime
		/// </summary>
		/// <param name="unixTimeStamp">The number of seconds since the Unix Epoch.</param>
		internal static DateTime ToDateTime(this double unixTimeStamp)
		{
			// Unix timestamp is seconds past epoch
			var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
			return dtDateTime;
		}

		/// <summary>
		/// Converts a DateTime to a Unix Timestamp
		/// </summary>
		/// <param name="dateTime">A datetime needed to be converted to a Unix Timestmap</param>
		internal static double ToUnixTimestamp(this DateTime dateTime)
		{
			return (dateTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
		}
	}
}
