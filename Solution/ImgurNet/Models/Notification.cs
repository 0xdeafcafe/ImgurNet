using ImgurNet.Converters.Json;
using Newtonsoft.Json;

namespace ImgurNet.Models
{
	public class Notification<T> : DataModelBase
	{
		/// <summary>
		/// The Id of the notification.
		/// </summary>
		public string Id
		{
			get { return _id; }
			set { SetField(ref _id, value); }
		}
		private string _id;

		/// <summary>
		/// The Id of the account the Notification belongs to
		/// </summary>
		[JsonProperty("account_id")]
		public int AccountId
		{
			get { return _accountId; }
			set { SetField(ref _accountId, value); }
		}
		private int _accountId;


		/// <summary>
		/// Indicates if the response has been viewed
		/// </summary>
		[JsonConverter(typeof(BoolConverter))]
		public bool Viewed
		{
			get { return _viewed; }
			set { SetField(ref _viewed, value); }
		}
		private bool _viewed;

		/// <summary>
		/// The content of the notification
		/// </summary>
		public T Content
		{
			get { return _content; }
			set { SetField(ref _content, value); }
		}
		private T _content;

	}
}
