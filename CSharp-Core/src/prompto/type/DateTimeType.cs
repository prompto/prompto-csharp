using prompto.value;
using prompto.runtime;
using System;
using System.Collections.Generic;
using DateTime = prompto.value.DateTime;
using prompto.store;

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
			: base(TypeFamily.DATETIME)
        {
        }

		public override string GetTypeName()
		{
			return "DateTime";
		}

        
        public override Type ToCSharpType()
        {
            return typeof(DateTime);
        }


		public override IValue ConvertCSharpValueToIValue(Context context, Object value)
		{
			if (value is System.DateTimeOffset)
				return new DateTime((System.DateTimeOffset)value);
			else if (value is System.DateTime?)
				return new DateTime(((System.DateTimeOffset?)value).Value);
			else
				return base.ConvertCSharpValueToIValue(context, value);
		}


		public override IType checkMember(Context context, String name)
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
            else if ("millisecond" == name)
                return IntegerType.Instance;
            else if ("tzOffset" == name)
                return IntegerType.Instance;
            else if ("tzName" == name)
                return TextType.Instance;
            else if ("date" == name)
                return DateType.Instance;
            else if ("time" == name)
                return TimeType.Instance;
            else
                return base.checkMember(context, name);
        }

        
		public override IType checkAdd(Context context, IType other, bool tryReverse)
        {
            if (other is PeriodType)
                return this;
			return base.checkAdd(context, other, tryReverse);
        }

        
        public override IType checkSubstract(Context context, IType other)
        {
            if (other is DateTimeType)
                return PeriodType.Instance;
            else if (other is PeriodType)
                return this;
            else
				return base.checkSubstract(context, other);
        }

        
        public override IType checkCompare(Context context, IType other)
        {
            if (other is DateType)
                return BooleanType.Instance;
            if (other is DateTimeType)
                return BooleanType.Instance;
            return base.checkCompare(context, other);
        }

        override
		public ListValue sort(Context context, IContainer list, bool descending)
        {
			return this.doSort(context, list, new DateTimeComparer(context, descending));
        }

        override
        public String ToString(Object value)
        {
            return "'" + value.ToString() + "'";
        }

    }

    class DateTimeComparer : ExpressionComparer<DateTime>
    {
        public DateTimeComparer(Context context, bool descending)
            : base(context, descending)
        {
        }

        override
        protected int DoCompare(DateTime o1, DateTime o2)
        {
            return o1.CompareTo(o2);
        }
    }

}
