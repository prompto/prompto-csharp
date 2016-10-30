using System;
using prompto.runtime;
using prompto.store;

namespace prompto.type
{

	public class NullType : BaseType
	{

		static NullType instance_ = new NullType ();

		public static NullType Instance {
			get {
				return instance_;
			}
		}

		private NullType ()
			: base (TypeFamily.NULL)
		{
		}

		public override Type ToCSharpType ()
		{
			return null;
		}

		public override void checkUnique (Context context)
		{
			// ok
		}

		public override void checkExists (Context context)
		{
			// ok
		}

		public override bool isAssignableTo (Context context, IType other)
		{
			return true;
		}

		public override bool isMoreSpecificThan (Context context, IType other)
		{
			return false;
		}



	}
}
