using prompto.runtime;
using prompto.store;
using prompto.value;

namespace prompto.type
{

	public class CursorType : ContainerType
	{

		public CursorType (IType itemType)
			: base (TypeFamily.CURSOR, itemType, itemType.GetTypeName() + "[]")
		{
		}

		public override System.Type ToCSharpType ()
		{
			return typeof(Cursor);
		}

		public override bool isAssignableFrom(Context context, IType other)
		{
			return base.isAssignableFrom(context, other)
					   || (other is CursorType && itemType.isAssignableFrom(context, ((CursorType)other).GetItemType()));
		}

		public override bool Equals (object obj)
		{
			if (obj == this)
				return true; 
			if (!(obj is CursorType))
				return false;
			CursorType other = (CursorType)obj;
			return this.GetItemType ().Equals (other.GetItemType ());
		}

		public override IType checkIterator (Context context)
		{
			return itemType;
		}

		public override IType checkMember (Context context, string name)
		{
			if ("count".Equals (name))
				return IntegerType.Instance;
			else if ("totalCount".Equals(name))
				return IntegerType.Instance;
			else
				return base.checkMember (context, name);
		}


	}
}