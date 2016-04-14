using prompto.runtime;
using prompto.value;
using System;

namespace prompto.type
{


	public class SetType : ContainerType
	{

		public SetType (IType itemType)
			: base (itemType.GetName () + "<>", itemType)
		{
		}

		public override IType checkIterator (Context context)
		{
			return itemType;
		}

		public override IType checkAdd (Context context, IType other, bool tryReverse)
		{
			if (other is ContainerType) {
				IType itemType = ((ContainerType)other).GetItemType ();
				if ((other is ListType || other is SetType)
					&& this.GetItemType ().Equals (itemType))
					return this;
			} 
			return base.checkAdd (context, other, tryReverse);
		}


		public override IType CheckMember(Context context, String name)
		{
			if ("length" == name)
				return IntegerType.Instance;
			else
				return base.CheckMember(context, name);
		}

		public override IType checkItem (Context context, IType other)
		{
			if (other == IntegerType.Instance )
				return itemType;
			else
				return base.checkItem (context, other);
		}

		public override IType checkContainsAllOrAny (Context context, IType other)
		{
			return BooleanType.Instance;
		}

		public override System.Type ToCSharpType ()
		{
			return typeof(SetValue);
		}

	}
}