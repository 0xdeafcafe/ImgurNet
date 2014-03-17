using System;

namespace ImgurNet.Helpers
{
	internal static class EnumUtilities
	{
		internal static T ParseEnum<T>(string value)
		{
			return (T) Enum.Parse(typeof (T), value, true);
		}
	}
}
