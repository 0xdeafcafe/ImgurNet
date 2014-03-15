using Newtonsoft.Json;

namespace ImgurNet.Models
{
	public class Error : DataModelBase
	{
		/// <summary>
		/// The description of the error.
		/// </summary>
		[JsonProperty("error")]
		public string ErrorDescription
		{
			get { return _error; }
			set { SetField(ref _error, value); }
		}
		private string _error;
		
		/// <summary>
		/// The Api endpoint that was called
		/// </summary>
		public string Request
		{
			get { return _request; }
			set { SetField(ref _request, value); }
		}
		private string _request;
		
		/// <summary>
		/// The HTTP method that was used on the request
		/// </summary>
		public string Method
		{
			get { return _method; }
			set { SetField(ref _method, value); }
		}
		private string _method;
	}
}
