using System;
using ImgurNet.Models;

namespace ImgurNet.Exceptions
{
	public class ImgurResponseFailedException : Exception
	{
		public ImgurResponse<Error> ImgurResponse; 

		public ImgurResponseFailedException(string message)
			: base(message) { }

		public ImgurResponseFailedException(string message, Exception innerException)
			: base(message, innerException) { }

		public ImgurResponseFailedException(ImgurResponse<Error> imgurResponse, string message)
			: base(message)
		{
			ImgurResponse = imgurResponse;
		}

		public ImgurResponseFailedException(ImgurResponse<Error> imgurResponse, string message, Exception innerException)
			: base(message, innerException)
		{
			ImgurResponse = imgurResponse;
		}
	}
}
