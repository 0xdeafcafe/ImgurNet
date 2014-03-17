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
		/// The Id of the account sending the message
		/// </summary>
		[JsonProperty("account_id")]
		public int AccountId
		{
			get { return _accountId; }
			set { SetField(ref _accountId, value); }
		}
		private int _accountId;

		/// <summary>
		/// ?
		/// </summary>
		[JsonProperty("with_account")]
		public int WithAccount
		{
			get { return _withAccount; }
			set { SetField(ref _withAccount, value); }
		}
		private int _withAccount;

		/// <summary>
		/// ?
		/// </summary>
		public int Spam
		{
			get { return _spam; }
			set { SetField(ref _spam, value); }
		}
		private int _spam;

		/// <summary>
		/// ?
		/// </summary>
		[JsonProperty("message_num")]
		public int MessageNumber
		{
			get { return _messageNumber; }
			set { SetField(ref _messageNumber, value); }
		}
		private int _messageNumber;
		
		/// <summary>
		/// The body of the message
		/// </summary>
		[JsonProperty("last_message")]
		public string LastMessage
		{
			get { return _lastMessage; }
			set { SetField(ref _lastMessage, value); }
		}
		private string _lastMessage;

		/// <summary>
		/// The username of the person that sent the message
		/// </summary>
		public string From
		{
			get { return _from; }
			set { SetField(ref _from, value); }
		}
		private string _from;

		/// <summary>
		/// When the message was sent
		/// </summary>
		[JsonConverter(typeof(UnixDateTimeConverter))]
		[JsonProperty("datetime")]
		public DateTime Created
		{
			get { return _created; }
			set { SetField(ref _created, value); }
		}
		private DateTime _created;
	}
}
