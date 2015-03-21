

using presto.runtime;
using System;
using System.Collections.Generic;
using presto.value;

namespace presto.type
{

    public class ListType : CollectionType
    {

        public ListType(IType itemType)
            : base(itemType.getName() + "[]", itemType)
        {
        }

        override
        public System.Type ToSystemType()
        {
            return typeof(ListValue);
        }

        override
        public IType CheckMember(Context context, String name)
        {
            if ("length" == name)
                return IntegerType.Instance;
            else
                return base.CheckMember(context, name);
        }
        
        override
        public bool isAssignableTo(Context context, IType other)
        {
            return (other is ListType) && itemType.isAssignableTo(context, ((ListType)other).GetItemType());
        }

        override
        public bool Equals(Object obj)
        {
            if (obj == this)
                return true;
            if (obj == null)
                return false;
            if (!(obj is ListType))
                return false;
            ListType other = (ListType)obj;
            return this.GetItemType().Equals(other.GetItemType());
        }

        
		public override IType checkAdd(Context context, IType other, bool tryReverse)
        {
            if (other is ListType
                && this.GetItemType().Equals(((ListType)other).GetItemType()))
                return this;
            else
				return base.checkAdd(context, other, tryReverse);
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

        
		public override IType checkMultiply(Context context, IType other, bool tryReverse)
        {
            if (other is IntegerType)
                return this;
			return base.checkMultiply(context, other, tryReverse);
        }

        override
        public IType checkContainsAllOrAny(Context context, IType other)
        {
            return BooleanType.Instance;
        }

        override
        public IType checkIterator(Context context)
        {
            return itemType;
        }

    }

}
