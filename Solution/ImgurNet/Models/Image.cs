using System;
using ImgurNet.Converters.Json;
using Newtonsoft.Json;

namespace ImgurNet.Models
{
	public class Image : DataModelBase
	{
		/// <summary>
		/// The Id for the image.
		/// </summary>
		public string Id
		{
			get { return _id; }
			set { SetField(ref _id, value); }
		}
		private string _id;

		/// <summary>
		/// The title of the image.
		/// </summary>
		public string Title
		{
			get { return _title; }
			set { SetField(ref _title, value); }
		}
		private string _title;

		/// <summary>
		/// Description of the image.
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
		/// Image MIME type.
		/// </summary>
		public string Type
		{
			get { return _type; }
			set { SetField(ref _type, value); }
		}
		private string _type;

		/// <summary>
		/// Is the image animated.
		/// </summary>
		public bool Animated
		{
			get { return _animated; }
			set { SetField(ref _animated, value); }
		}
		private bool _animated;

		/// <summary>
		/// The width of the image in pixels.
		/// </summary>
		public int Width
		{
			get { return _width; }
			set { SetField(ref _width, value); }
		}
		private int _width;

		/// <summary>
		/// The height of the image in pixels.
		/// </summary>
		public int Height
		{
			get { return _height; }
			set { SetField(ref _height, value); }
		}
		private int _height;

		/// <summary>
		/// The size of the image.
		/// </summary>
		public int Size
		{
			get { return _size; }
			set { SetField(ref _size, value); }
		}
		private int _size;

		/// <summary>
		/// The number of views the image has.
		/// </summary>
		public int Views
		{
			get { return _views; }
			set { SetField(ref _views, value); }
		}
		private int _views;

		/// <summary>
		/// Bandwidth consumed by the image in bytes.
		/// </summary>
		public Int64 Bandwidth
		{
			get { return _bandwidth; }
			set { SetField(ref _bandwidth, value); }
		}
		private Int64 _bandwidth;

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
		/// If the image has been categorized by their backend then this will contain the section the image belongs in. (funny, cats, adviceanimals, wtf, etc).
		/// </summary>
		public string Section
		{
			get { return _section; }
			set { SetField(ref _section, value); }
		}
		private string _section;

		/// <summary>
		/// The direct link to the image.
		/// </summary>
		public string Link
		{
			get { return _link; }
			set { SetField(ref _link, value); }
		}
		private string _link;

		/// <summary>
		/// Gets a link to the image, as the specified thumbnail size.
		/// </summary>
		/// <param name="imageThumbnailSize">The tnumbnail size you want. Size guide here: (https://api.imgur.com/models/image#thumbs)</param>
		/// <returns>A link to the image, with the specified thumbnail indictorator embedded.</returns>
		public string GetThumbnailLink(ImageThumbnailSize imageThumbnailSize)
		{
			var lastDecimalIndex = Link.LastIndexOf('.');
			if (lastDecimalIndex < 0)
				throw new IndexOutOfRangeException("there was no decimal point in the url. There should be.");

			char thumbnailSize;
			switch (imageThumbnailSize)
			{
				default:
				case ImageThumbnailSize.SmallSquare:
					thumbnailSize = 's';
					break;
				case ImageThumbnailSize.BigSquare:
					thumbnailSize = 'b';
					break;
				case ImageThumbnailSize.SmallThumbnail:
					thumbnailSize = 't';
					break;
				case ImageThumbnailSize.MediumThumbnail:
					thumbnailSize = 'm';
					break;
				case ImageThumbnailSize.LargeThumbnail:
					thumbnailSize = 'l';
					break;
				case ImageThumbnailSize.HugeThumbnail:
					thumbnailSize = 'h';
					break;
			}

			return Link.Insert(lastDecimalIndex, thumbnailSize.ToString());
		}
	}
}
