using System.Net;

namespace ImgurNet.Models
{
	public class ImgurResponse<T> : NotifyPropertyChangedBase
		where T : DataModelBase
	{
		/// <summary>
		/// 
		/// </summary>
		public T Data
		{
			get { return _data; }
			set { SetField(ref _data, value); }
		}
		private T _data;

		/// <summary>
		/// 
		/// </summary>
		public HttpStatusCode Status
		{
			get { return _status; }
			set { SetField(ref _status, value); }
		}
		private HttpStatusCode _status;

		/// <summary>
		/// 
		/// </summary>
		public bool Success
		{
			get { return _success; }
			set { SetField(ref _success, value); }
		}
		private bool _success;
	}
}
