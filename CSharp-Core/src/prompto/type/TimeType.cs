using prompto.error;
using prompto.runtime;
using prompto.value;
using System;
using System.Collections.Generic;
using prompto.store;

namespace prompto.type
{

    public class TimeType : NativeType
    {

        static TimeType instance = new TimeType();

        public static TimeType Instance
        {
            get
            {
                return instance;
            }
        }

        private TimeType()
			: base(TypeFamily.TIME)
        {
        }

        override 
        public System.Type ToCSharpType()
        {
            return typeof(Time);
        }


        
		public override IType checkMember(Context context, String name)
        {
            if ("hour" == name)
                return IntegerType.Instance;
            else if ("minute" == name)
                return IntegerType.Instance;
            else if ("second" == name)
                return IntegerType.Instance;
            else if ("millisecond" == name)
                return IntegerType.Instance;
            else
                return base.checkMember(context, name);
        }
        
        override 
        public bool isAssignableTo(Context context, IType other)
        {
            return (other is TimeType) || (other is AnyType);
        }

         
		public override IType checkAdd(Context context, IType other, bool tryReverse)
        {
            if (other is PeriodType)
                return DateTimeType.Instance;
			return base.checkAdd(context, other, tryReverse);
        }

        override 
        public IType checkSubstract(Context context, IType other)
        {
            if (other is TimeType)
                return PeriodType.Instance;
            if (other is PeriodType)
                return this;
            return base.checkSubstract(context, other);
        }

        override 
        public IType checkCompare(Context context, IType other)
        {
            if (other is TimeType)
                return BooleanType.Instance;
            return base.checkCompare(context, other);
        }

        override
        public IType checkRange(Context context, IType other)
        {
            if (other is TimeType)
                return new RangeType(this);
            return base.checkRange(context, other);
        }

        override
        public IRange newRange(Object left, Object right)
        {
            if (left is Time && right is Time)
                return new TimeRange((Time)left, (Time)right);
            return base.newRange(left, right);
        }

        override 
		public ListValue sort(Context context, IContainer list, bool descending)
        {
            return this.doSort(context, list, new TimeComparer(context, descending));
        }

        override
        public String ToString(Object value)
        {
            return "'" + value.ToString() + "'";
        }

    }

    class TimeComparer : ExpressionComparer<Time>
    {
        public TimeComparer(Context context, bool descending)
            : base(context, descending)
        {
        }

        override
        protected int DoCompare(Time o1, Time o2)
        {
            return o1.CompareTo(o2);
        }
    }

}