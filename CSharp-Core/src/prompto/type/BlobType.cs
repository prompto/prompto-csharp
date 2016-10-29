using prompto.runtime;

namespace prompto.type
{
	
	public class BlobType : BinaryType
	{

		static BlobType instance_ = new BlobType ();

		public static BlobType Instance {
			get {
				return instance_;
			}
		}


		private BlobType ()
			: base (TypeFamily.BLOB)
		{
		}

		public override bool isAssignableTo (Context context, IType other)
		{
			return (other is BlobType);
		}


	}
}