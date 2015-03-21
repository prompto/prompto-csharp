using presto.value;
using presto.runtime;
using System;
using System.Collections.Generic;
using presto.error;
namespace presto.type
{

    public class DateType : NativeType
    {

        static DateType instance_ = new DateType();

        public static DateType Instance
        {
            get
            {
                return instance_;
            }
        }

        private DateType()
            : base("Date")
        {
        }

        override
        public Type ToSystemType()
        {
            return typeof(Date);
        }


        override
        public bool isAssignableTo(Context context, IType other)
        {
            return (other is DateType) || (other is AnyType);
        }

        override
		public IType checkAdd(Context context, IType other, bool tryReverse)
        {
            if (other is PeriodType)
                return this; // ignore time section
			return base.checkAdd(context, other, tryReverse);
        }

        override
        public IType checkSubstract(Context context, IType other)
        {
            if (other is PeriodType)
                return this; // ignore time section
            else if (other is DateType)
                return PeriodType.Instance;
            else if (other is TimeType)
                return PeriodType.Instance;
            else if (other is DateTimeType)
                return PeriodType.Instance;
            return base.checkSubstract(context, other);
        }

        override
        public IType checkCompare(Context context, IType other)
        {
            if (other is DateType)
                return BooleanType.Instance;
            if (other is DateTimeType)
                return BooleanType.Instance;
            return base.checkCompare(context, other);
        }

        override
        public IType checkRange(Context context, IType other)
        {
            if (other is DateType)
                return new RangeType(this);
            return base.checkRange(context, other);
        }

        override
        public IType CheckMember(Context context, String name)
        {
            if ("year" == name)
                return IntegerType.Instance;
            else if ("month" == name)
                return IntegerType.Instance;
            else if ("dayOfMonth" == name)
                return IntegerType.Instance;
            else if ("dayOfYear" == name)
                return IntegerType.Instance;
            else
                return base.CheckMember(context, name);
        }

        override
        public IRange newRange(Object left, Object right)
        {
            if (left is Date && right is Date)
                return new DateRange((Date)left, (Date)right);
            return base.newRange(left, right);
        } 

        override
		public ListValue sort(Context context, IContainer list)
        {
			return this.doSort(context, list, new DateComparer(context));
        }

        override
        public String ToString(Object value)
        {
            return "'" + value.ToString() + "'";
        }
    }

    class DateComparer : ExpressionComparer<Date>
    {
        public DateComparer(Context context)
            : base(context)
        {
        }
        override
        protected int DoCompare(Date o1, Date o2)
        {
            return o1.getMillis().CompareTo(o2.getMillis());
        }
    }

}
