using prompto.runtime;
using System;
using prompto.store;

namespace prompto.type
{

    public class RangeType : ContainerType
    {

        public RangeType(IType itemType)
			: base(TypeFamily.RANGE, itemType, itemType.GetTypeName() + "[..]")
        {
        }


		public override IterableType WithItemType(IType itemType)
		{
			return new RangeType(itemType);
		}

        
        public override Type ToCSharpType(Context context)
        {
            return null; // no equivalent
        }


        override
        public bool Equals(Object obj)
        {
            if (obj == this)
                return true;
            if (obj == null)
                return false;
            if (!(obj is RangeType))
                return false;
            RangeType other = (RangeType)obj;
            return this.GetItemType().Equals(other.GetItemType());
        }

        
        public override IType checkItem(Context context, IType other)
        {
            if (other == IntegerType.Instance)
                return itemType;
            else
                return base.checkItem(context, other);
        }

        
        public override IType checkSlice(Context context)
        {
            return this;
        }

        
        public override IType checkIterator(Context context)
        {
            return itemType;
        }

		public override void checkContainsAllOrAny(Context context, IType other)
		{
			// nothing to do
		}

    }

}
