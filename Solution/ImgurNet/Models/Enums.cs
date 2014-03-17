namespace ImgurNet.Models
{
	public class Enums
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
	}
}
