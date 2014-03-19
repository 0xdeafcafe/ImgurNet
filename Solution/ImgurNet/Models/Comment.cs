using System;
using ImgurNet.Converters.Json;
using Newtonsoft.Json;

namespace ImgurNet.Models
{
	public class Comment : DataModelBase
	{
		/// <summary>
		/// The Id of the comment
		/// </summary>
		public int Id
		{
			get { return _id; }
			set { SetField(ref _id, value); }
		}
		private int _id;

		/// <summary>
		/// The Id of the image that the comment is for
		/// </summary>
		[JsonProperty("image_id")]
		public string ImageId
		{
			get { return _imageId; }
			set { SetField(ref _imageId, value); }
		}
		private string _imageId;

		/// <summary>
		/// The body of the comment
		/// </summary>
		[JsonProperty("comment")]
		public string Caption
		{
			get { return _caption; }
			set { SetField(ref _caption, value); }
		}
		private string _caption;

		/// <summary>
		/// The username of the author of the comment
		/// </summary>
		public string Author
		{
			get { return _author; }
			set { SetField(ref _author, value); }
		}
		private string _author;

		/// <summary>
		/// The username of the author of the comment
		/// </summary>
		[JsonProperty("author_id")]
		public int AuthorId
		{
			get { return _authorId; }
			set { SetField(ref _authorId, value); }
		}
		private int _authorId;

		/// <summary>
		/// If this comment was done to an album
		/// </summary>
		[JsonProperty("on_album")]
		public bool OnAlbum
		{
			get { return _onAlbum; }
			set { SetField(ref _onAlbum, value); }
		}
		private bool _onAlbum;

		/// <summary>
		/// The Id of the album cover image, this is what should be displayed for album comments
		/// </summary>
		[JsonProperty("album_cover")]
		public string AlbumCover
		{
			get { return _albumCover; }
			set { SetField(ref _albumCover, value); }
		}
		private string _albumCover;

		/// <summary>
		/// The number of upvotes for the comment
		/// </summary>
		[JsonProperty("ups")]
		public int UpVotes
		{
			get { return _upVotes; }
			set { SetField(ref _upVotes, value); }
		}
		private int _upVotes;

		/// <summary>
		/// The number of downvotes for the comment
		/// </summary>
		[JsonProperty("downs")]
		public int DownVotes
		{
			get { return _downVotes; }
			set { SetField(ref _downVotes, value); }
		}
		private int _downVotes;

		/// <summary>
		/// The number of upvotes - downvotes
		/// </summary>
		public float Points
		{
			get { return _points; }
			set { SetField(ref _points, value); }
		}
		private float _points;

		/// <summary>
		/// When the comment was created
		/// </summary>
		[JsonConverter(typeof(UnixDateTimeConverter))]
		[JsonProperty("datetime")]
		public DateTime Created
		{
			get { return _created; }
			set { SetField(ref _created, value); }
		}
		private DateTime _created;

		/// <summary>
		/// If this is a reply, this will be the value of the Id for the comemnt this is a reply for.
		/// </summary>
		[JsonProperty("parent_id")]
		public int ParentId
		{
			get { return _parentId; }
			set { SetField(ref _parentId, value); }
		}
		private int _parentId;

		/// <summary>
		/// Marked true if this caption has been deleted
		/// </summary>
		public bool Deleted
		{
			get { return _deleted; }
			set { SetField(ref _deleted, value); }
		}
		private bool _deleted;

		/// <summary>
		/// The authenticated user's vote (up/down/null)
		/// </summary>
		[JsonProperty("vote")]
		public string YourVote
		{
			get { return _yourVote; }
			set { SetField(ref _yourVote, value); }
		}
		private string _yourVote;

		/// <summary>
		/// All of the replies for this comment. If there are no replies to the comment then this is an empty set.
		/// </summary>
		public Comment[] Children
		{
			get { return _children; }
			set { SetField(ref _children, value); }
		}
		private Comment[] _children;
	}
}
