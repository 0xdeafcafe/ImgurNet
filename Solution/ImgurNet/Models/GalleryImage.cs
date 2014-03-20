using Newtonsoft.Json;

namespace ImgurNet.Models
{
	public class GalleryImage : Image, IGalleryObject
	{
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("ups")]
		public int Upvotes
		{
			get { return _upvotes; }
			set { SetField(ref _upvotes, value); }
		}
		private int _upvotes;

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("downs")]
		public int Downvotes
		{
			get { return _downvotes; }
			set { SetField(ref _downvotes, value); }
		}
		private int _downvotes;

		/// <summary>
		/// 
		/// </summary>
		public int Score
		{
			get { return _score; }
			set { SetField(ref _score, value); }
		}
		private int _score;

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("is_album")]
		public bool IsAlbum
		{
			get { return _isAlbum; }
			set { SetField(ref _isAlbum, value); }
		}
		private bool _isAlbum;
	}
}
