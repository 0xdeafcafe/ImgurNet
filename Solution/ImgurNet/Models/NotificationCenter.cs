namespace ImgurNet.Models
{
	public class NotificationCenter : DataModelBase
	{
		/// <summary>
		/// The accounts notifications for replies
		/// </summary>
		public Notification<Comment>[] Replies
		{
			get { return _replies; }
			set { SetField(ref _replies, value); }
		}
		private Notification<Comment>[] _replies;

		/// <summary>
		/// The accounts notifications for messages
		/// </summary>
		public Notification<Message>[] Messages
		{
			get { return _messages; }
			set { SetField(ref _messages, value); }
		}
		private Notification<Message>[] _messages;
	}
}
