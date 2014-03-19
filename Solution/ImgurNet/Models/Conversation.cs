using System;
using ImgurNet.Converters.Json;
using Newtonsoft.Json;

namespace ImgurNet.Models
{
	public class Conversation : DataModelBase
	{
		/// <summary>
		/// The Id of the message
		/// </summary>
		public int Id
		{
			get { return _id; }
			set { SetField(ref _id, value); }
		}
		private int _id;

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("with_account")]
		public string SenderAccount
		{
			get { return _senderAccount; }
			set { SetField(ref _senderAccount, value); }
		}
		private string _senderAccount;

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("with_account_id")]
		public int SenderAccountId
		{
			get { return _senderAccountId; }
			set { SetField(ref _senderAccountId, value); }
		}
		private int _senderAccountId;

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("last_message_preview")]
		public string LastMessagePreview
		{
			get { return _lastMessagePreview; }
			set { SetField(ref _lastMessagePreview, value); }
		}
		private string _lastMessagePreview;

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("message_count")]
		public int MessageCount
		{
			get { return _messageCount; }
			set { SetField(ref _messageCount, value); }
		}
		private int _messageCount;

		/// <summary>
		/// 
		/// </summary>
		[JsonConverter(typeof(UnixDateTimeConverter))]
		[JsonProperty("datetime")]
		public DateTime DateTime
		{
			get { return _dateTime; }
			set { SetField(ref _dateTime, value); }
		}
		private DateTime _dateTime;

		/// <summary>
		/// 
		/// </summary>
		public Message[] Messages
		{
			get { return _messages; }
			set { SetField(ref _messages, value); }
		}
		private Message[] _messages;
	}
}
