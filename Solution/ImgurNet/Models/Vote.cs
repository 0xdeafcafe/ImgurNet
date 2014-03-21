using Newtonsoft.Json;

namespace ImgurNet.Models
{
	public class Vote : DataModelBase
	{
		/// <summary>
		/// Number of upvotes
		/// </summary>
		[JsonProperty("ups")]
		public int UpVotes
		{
			get { return _upVotes; }
			set { SetField(ref _upVotes, value); }
		}
		private int _upVotes;

		/// <summary>
		/// Number of downvotes
		/// </summary>
		[JsonProperty("downs")]
		public int DownVotes
		{
			get { return _downVotes; }
			set { SetField(ref _downVotes, value); }
		}
		private int _downVotes;
	}
}
