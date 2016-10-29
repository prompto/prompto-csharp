using prompto.runtime;
using System;
namespace prompto.type
{

    public class RangeType : ContainerType
    {

        public RangeType(IType itemType)
			: base(TypeFamily.RANGE, itemType, itemType.GetTypeName() + "[..]")
        {
        }

        override
        public bool isAssignableTo(Context context, IType other)
        {
            return this.Equals(other);
        }

        override
        public System.Type ToCSharpType()
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

        override
        public IType checkItem(Context context, IType other)
        {
            if (other == IntegerType.Instance)
                return itemType;
            else
                return base.checkItem(context, other);
        }

        override
        public IType checkSlice(Context context)
        {
            return this;
        }

        override
        public IType checkIterator(Context context)
        {
            return itemType;
        }

		public override IType checkContainsAllOrAny(Context context, IType other)
		{
			return BooleanType.Instance;
		}

    }

}
