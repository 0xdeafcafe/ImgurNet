using System;
using ImgurNet.Converters.Json;
using Newtonsoft.Json;

namespace ImgurNet.Models
{
	public class Credits : DataModelBase
	{
		internal const string CreditsUrl = "credits";

		/// <summary>
		/// The number of requests the client can make
		/// </summary>
		public int ClientLimit
		{
			get { return _clientLimit; }
			set { SetField(ref _clientLimit, value); }
		}
		private int _clientLimit;

		/// <summary>
		/// The numer of requests the client has left
		/// </summary>
		public int ClientRemaining
		{
			get { return _clientRemaining; }
			set { SetField(ref _clientRemaining, value); }
		}
		private int _clientRemaining;

		/// <summary>
		/// The number of requests the user can make using the client
		/// </summary>
		public int UserLimit
		{
			get { return _userLimit; }
			set { SetField(ref _userLimit, value); }
		}
		private int _userLimit;

		/// <summary>
		/// The number of requests the user has left using the client
		/// </summary>
		public int UserRemaining
		{
			get { return _userRemaining; }
			set { SetField(ref _userRemaining, value); }
		}
		private int _userRemaining;

		/// <summary>
		/// When the user's request count resets
		/// </summary>
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime UserReset
		{
			get { return _userReset; }
			set { SetField(ref _userReset, value); }
		}
		private DateTime _userReset;

		/// <summary>
		/// If the client can make any more requests
		/// </summary>
		public bool ClientHasMoreRequests
		{
			get { return ClientRemaining > 0; }
		}

		/// <summary>
		/// If the user can make any more requests
		/// </summary>
		public bool UserHasMoreRequests
		{
			get { return UserRemaining > 0; }
		}
	}
}
