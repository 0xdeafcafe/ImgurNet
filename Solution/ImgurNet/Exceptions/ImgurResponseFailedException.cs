using System;
using ImgurNet.Models;

namespace ImgurNet.Exceptions
{
	public class ImgurResponseFailedException<T> : Exception
		where T : DataModelBase
	{
		public ImgurResponse<T> ImgurResponse; 

		public ImgurResponseFailedException(string message)
			: base(message) { }

		public ImgurResponseFailedException(ImgurResponse<T> imgurResponse, string message)
			: base(message)
		{
			ImgurResponse = imgurResponse;
		}

		public ImgurResponseFailedException(ImgurResponse<T> imgurResponse, string message, Exception innerException)
			: base(message, innerException)
		{
			ImgurResponse = imgurResponse;
		}
	}
}
