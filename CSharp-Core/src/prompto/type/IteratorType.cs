using System;
using prompto.runtime;
using prompto.value;

namespace prompto.type
{
	
public class IteratorType : IterableType {

	public IteratorType(IType itemType)
		: base("Iterator<" + itemType.GetName() + ">", itemType)
		{
	}

		public override System.Type ToCSharpType ()
		{
			return typeof(IterableValue);
		}

		public override bool isAssignableTo(Context context, IType other) {
			return (other is IteratorType) && itemType.isAssignableTo(context, ((IteratorType)other).GetItemType());
	}

		public override bool Equals(Object obj) {
		if(obj==this)
			return true; 
		if(!(obj is IteratorType))
			return false;
		IteratorType other = (IteratorType)obj;
		return this.GetItemType().Equals(other.GetItemType());
	}

		public override IType checkIterator(Context context) {
		return itemType;
	}

		public override IType CheckMember(Context context, String name) {
		if ("length"==name)
			return IntegerType.Instance;
		else
				return base.CheckMember(context, name);
	}

	}
}
