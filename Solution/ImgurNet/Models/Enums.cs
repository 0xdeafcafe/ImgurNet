namespace ImgurNet.Models
{
	public enum ImageThumbnailSize
	{
		SmallSquare,
		BigSquare,
		SmallThumbnail,
		MediumThumbnail,
		LargeThumbnail,
		HugeThumbnail
	}

	public enum OAuth2Type
	{
		Code,
		Token,
		Pin
	}

	public enum Privacy
	{
		Public,
		Hidden,
		Secret
	}

	public enum AlbumLayout
	{
		Blog,
		Grid,
		Horizontal,
		Vertical
	}

	public enum VoteDirection
	{
		Up,
		Down
	}

	public enum GallerySection
	{
		Hot,
		Top,
		User
	}

	public enum GallerySort
	{
		Viral,
		Time,
		Top
	}

	public enum GalleryWindow
	{
		Day,
		Week,
		Month,
		Year,
		All
	}
}
