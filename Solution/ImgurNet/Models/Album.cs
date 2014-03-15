using System;
using ImgurNet.Converters.Json;
using Newtonsoft.Json;

namespace ImgurNet.Models
{
	public class Album : DataModelBase
	{
		/// <summary>
		/// The Id for the album.
		/// </summary>
		public string Id
		{
			get { return _id; }
			set { SetField(ref _id, value); }
		}
		private string _id;

		/// <summary>
		/// The title of the album in the gallery
		/// </summary>
		public string Title
		{
			get { return _title; }
			set { SetField(ref _title, value); }
		}
		private string _title;

		/// <summary>
		/// The description of the album in the gallery
		/// </summary>
		public string Description
		{
			get { return _description; }
			set { SetField(ref _description, value); }
		}
		private string _description;

		/// <summary>
		/// Time inserted into the gallery.
		/// </summary>
		[JsonProperty("datetime")]
		[JsonConverter(typeof(UnixDateTimeConverter))]
		public DateTime AddedToGallery
		{
			get { return _addedToGallery; }
			set { SetField(ref _addedToGallery, value); }
		}
		private DateTime _addedToGallery;

		/// <summary>
		/// The Id of the album cover image
		/// </summary>
		public string Cover
		{
			get { return _cover; }
			set { SetField(ref _cover, value); }
		}
		private string _cover;

		/// <summary>
		/// The width of the album cover image.
		/// </summary>
		[JsonProperty("cover_width")]
		public int CoverWidth
		{
			get { return _coverWidth; }
			set { SetField(ref _coverWidth, value); }
		}
		private int _coverWidth;

		/// <summary>
		/// The height of the album cover image.
		/// </summary>
		[JsonProperty("cover_height")]
		public int CoverHeight
		{
			get { return _coverHeight; }
			set { SetField(ref _coverHeight, value); }
		}
		private int _coverHeight;

		/// <summary>
		/// The account username or <see cref="Nullable"/> if it's anonymous.
		/// </summary>
		[JsonProperty("account_url")]
		public string AccountUrl
		{
			get { return _accountUrl; }
			set { SetField(ref _accountUrl, value); }
		}
		private string _accountUrl;

		/// <summary>
		/// The privacy level of the album, you can only view public if not logged in as album owner
		/// </summary>
		public string Privacy
		{
			get { return _privacy; }
			set { SetField(ref _privacy, value); }
		}
		private string _privacy;

		/// <summary>
		/// The view layout of the album.
		/// </summary>
		public string Layout
		{
			get { return _layout; }
			set { SetField(ref _layout, value); }
		}
		private string _layout;
		
		/// <summary>
		/// The number of album views
		/// </summary>
		public int Views
		{
			get { return _views; }
			set { SetField(ref _views, value); }
		}
		private int _views;

		/// <summary>
		/// The direct link to the album.
		/// </summary>
		public string Link
		{
			get { return _link; }
			set { SetField(ref _link, value); }
		}
		private string _link;

		/// <summary>
		/// OPTIONAL, the deletehash, if you're logged in as the image owner.
		/// </summary>
		public string DeleteHash
		{
			get { return _deleteHash; }
			set { SetField(ref _deleteHash, value); }
		}
		private string _deleteHash;

		/// <summary>
		/// The total number of images in the album (only available when requesting the direct album)
		/// </summary>
		[JsonProperty("images_count")]
		public int ImagesCount
		{
			get { return _imagesCount; }
			set { SetField(ref _imagesCount, value); }
		}
		private int _imagesCount;

		/// <summary>
		/// An array of all the images in the album (only available when requesting the direct album)
		/// </summary>
		public Image[] Images
		{
			get { return _images; }
			set { SetField(ref _images, value); }
		}
		private Image[] _images;
	}
}
