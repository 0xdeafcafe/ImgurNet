using System;
using ImgurNet.Converters.Json;
using Newtonsoft.Json;

namespace ImgurNet.Models
{
	public class Message : DataModelBase
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
		public string From
		{
			get { return _from; }
			set { SetField(ref _from, value); }
		}
		private string _from;

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("account_id")]
		public int AccountId
		{
			get { return _accountId; }
			set { SetField(ref _accountId, value); }
		}
		private int _accountId;

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("sender_id")]
		public int SenderId
		{
			get { return _senderId; }
			set { SetField(ref _senderId, value); }
		}
		private int _senderId;

		/// <summary>
		/// 
		/// </summary>
		public string Body
		{
			get { return _body; }
			set { SetField(ref _body, value); }
		}
		private string _body;

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("conversation_id")]
		public int ConversationId
		{
			get { return _conversationId; }
			set { SetField(ref _conversationId, value); }
		}
		private int _conversationId;

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
	}
}
