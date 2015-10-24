using prompto.runtime;
using prompto.value;

namespace prompto.type
{

	public class CursorType : CollectionType
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

		public override IType  checkAdd (Context context, IType other, bool tryReverse)
		{
			if (other is CollectionType) {
				IType itemType = ((CollectionType)other).GetItemType ();
				if ((other is CursorType || other is SetType)
				  && this.GetItemType ().Equals (itemType))
					return this;
			} 
			return base.checkAdd (context, other, tryReverse);
		}

		public override IType checkItem (Context context, IType other)
		{
			if (other == IntegerType.Instance)
				return itemType;
			else
				return base.checkItem (context, other);
		}

		public override IType checkSlice (Context context)
		{
			return this;
		}

		public override IType checkMultiply (Context context, IType other, bool tryReverse)
		{
			if (other is IntegerType)
				return this;
			return base.checkMultiply (context, other, tryReverse);
		}

		public override IType checkContainsAllOrAny (Context context, IType other)
		{
			return BooleanType.Instance;
		}

		public override IType checkIterator (Context context)
		{
			return itemType;
		}

		public override IType CheckMember (Context context, string name)
		{
			if ("length".Equals (name))
				return IntegerType.Instance;
			else
				return base.CheckMember (context, name);
		}


	}
}