using prompto.runtime;

namespace prompto.type
{
	
	public class ImageType : BinaryType
	{

		static ImageType instance_ = new ImageType ();

		public static ImageType Instance {
			get {
				return instance_;
			}
		}


		private ImageType ()
			: base ("Image")
		{
		}

		public override bool isAssignableTo (Context context, IType other)
		{
			return (other is ImageType);
		}


	}
}