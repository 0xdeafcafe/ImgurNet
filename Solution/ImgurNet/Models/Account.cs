using System;
using ImgurNet.Converters.Json;
using Newtonsoft.Json;

namespace ImgurNet.Models
{
	public class Account : DataModelBase
	{
		/// <summary>
		/// The Id of the account.
		/// </summary>
		public int Id
		{
			get { return _id; }
			set { SetField(ref _id, value); }
		}
		private int _id;

		/// <summary>
		/// The account username, will be the same as requested in the Url.
		/// </summary>
		public string Url
		{
			get { return _url; }
			set { SetField(ref _url, value); }
		}
		private string _url;

		/// <summary>
		/// A basic description the user has filled out.
		/// </summary>
		public string Bio
		{
			get { return _bio; }
			set { SetField(ref _bio, value); }
		}
		private string _bio;

		/// <summary>
		/// The reputation for the account, in it's numerical format.
		/// </summary>
		public float Reputation
		{
			get { return _reputation; }
			set { SetField(ref _reputation, value); }
		}
		private float _reputation;

		/// <summary>
		/// The epoch time of account creation.
		/// </summary>
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime Created
		{
			get { return _created; }
			set { SetField(ref _created, value); }
		}
		private DateTime _created;

		/// <summary>
		/// 0 if not a pro user, their expiration date if they are.
		/// </summary>
		public int ProExpiration
		{
			get { return _proExpiration; }
			set { SetField(ref _proExpiration, value); }
		}
		private int _proExpiration;
	}
}
