using prompto.value;
using prompto.runtime;
using System;
using System.Collections.Generic;
using DateTime = prompto.value.DateTime;

namespace prompto.type
{

    public class DateTimeType : NativeType
    {

        static DateTimeType instance_ = new DateTimeType();
        
        public static DateTimeType Instance
        {
            get
            {
                return instance_;
            }
        }

        private DateTimeType()
            : base("DateTime")
        {
        }

        override
        public Type ToCSharpType()
        {
            return typeof(DateTime);
        }

		override
		public IValue ConvertCSharpValueToPromptoValue(Object value)
		{
			if (value is System.DateTimeOffset)
				return new DateTime((System.DateTimeOffset)value);
			else if (value is System.DateTime?)
				return new DateTime(((System.DateTimeOffset?)value).Value);
			else
				return base.ConvertCSharpValueToPromptoValue(value);
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
            else if ("hour" == name)
                return IntegerType.Instance;
            else if ("minute" == name)
                return IntegerType.Instance;
            else if ("second" == name)
                return IntegerType.Instance;
            else if ("millis" == name)
                return IntegerType.Instance;
            else if ("tzOffset" == name)
                return IntegerType.Instance;
            else if ("tzName" == name)
                return TextType.Instance;
            else
                return base.CheckMember(context, name);
        }

        override
        public bool isAssignableTo(Context context, IType other)
        {
            return (other is DateTimeType) || (other is DateType) || (other is TimeType) || (other is AnyType);
        }

        override
		public IType checkAdd(Context context, IType other, bool tryReverse)
        {
            if (other is PeriodType)
                return this;
			return base.checkAdd(context, other, tryReverse);
        }

        override
        public IType checkSubstract(Context context, IType other)
        {
            if (other is DateTimeType)
                return PeriodType.Instance;
            if (other is DateType)
                return PeriodType.Instance;
            if (other is TimeType)
                return PeriodType.Instance;
            if (other is PeriodType)
                return this;
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
		public ListValue sort(Context context, IContainer list)
        {
			return this.doSort(context, list, new DateTimeComparer(context));
        }

        override
        public String ToString(Object value)
        {
            return "'" + value.ToString() + "'";
        }

    }

    class DateTimeComparer : ExpressionComparer<DateTime>
    {
        public DateTimeComparer(Context context)
            : base(context)
        {
        }

        override
        protected int DoCompare(DateTime o1, DateTime o2)
        {
            return o1.CompareTo(o2);
        }
    }

}
