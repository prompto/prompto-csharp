using prompto.runtime;
using prompto.value;

namespace prompto.type
{

	public class CursorType : ContainerType
	{

		public CursorType (IType itemType)
			: base (itemType.GetName () + "[]", itemType)
		{
		}

		public override System.Type ToCSharpType ()
		{
			return typeof(Cursor);
		}

		public override bool isAssignableTo (Context context, IType other)
		{
			return (other is CursorType) && itemType.isAssignableTo (context, ((CursorType)other).GetItemType ());
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
			if ("length".Equals (name))
				return IntegerType.Instance;
			else
				return base.checkMember (context, name);
		}


	}
}