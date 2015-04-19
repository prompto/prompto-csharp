using presto.error;
using presto.runtime;
using presto.value;
using System;
using System.Collections.Generic;

namespace presto.type
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
            : base("Time")
        {
        }

        override 
        public System.Type ToCSharpType()
        {
            return typeof(Time);
        }


        override
        public IType CheckMember(Context context, String name)
        {
            if ("hour" == name)
                return IntegerType.Instance;
            else if ("minute" == name)
                return IntegerType.Instance;
            else if ("second" == name)
                return IntegerType.Instance;
            else if ("millis" == name)
                return IntegerType.Instance;
            else
                return base.CheckMember(context, name);
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
                return DateTimeType.Instance;
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
		public ListValue sort(Context context, IContainer list)
        {
            return this.doSort(context, list, new TimeComparer(context));
        }

        override
        public String ToString(Object value)
        {
            return "'" + value.ToString() + "'";
        }

    }

    class TimeComparer : ExpressionComparer<Time>
    {
        public TimeComparer(Context context)
            : base(context)
        {
        }

        override
        protected int DoCompare(Time o1, Time o2)
        {
            return o1.CompareTo(o2);
        }
    }

}