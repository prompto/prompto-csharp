using System;
using prompto.runtime;
using prompto.store;
using prompto.value;

namespace prompto.type
{

	public class IteratorType : IterableType
	{

		public IteratorType(IType itemType)
			: base(TypeFamily.ITERATOR, itemType, "Iterator<" + itemType.GetTypeName() + ">")
		{
		}

		public override IterableType WithItemType(IType itemType)
		{
			return new IteratorType(itemType);
		}

		public override Type ToCSharpType()
		{
			return typeof(IterableValue);
		}

		public override bool isAssignableFrom(Context context, IType other)
		{
			return base.isAssignableFrom(context, other)
					   || (other is IteratorType && itemType.isAssignableFrom(context,
									((IteratorType)other).GetItemType()));
		}

		public override bool Equals(Object obj)
		{
			if (obj == this)
				return true;
			if (!(obj is IteratorType))
				return false;
			IteratorType other = (IteratorType)obj;
			return this.GetItemType().Equals(other.GetItemType());
		}

		public override IType checkIterator(Context context)
		{
			return itemType;
		}

		public override IType checkMember(Context context, String name)
		{
			if ("count" == name)
				return IntegerType.Instance;
			else
				return base.checkMember(context, name);
		}

	}
}
