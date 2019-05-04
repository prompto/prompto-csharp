using prompto.value;
using prompto.runtime;
using System;
using System.Collections.Generic;
using prompto.error;
using prompto.store;

namespace prompto.type
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
			: base(TypeFamily.DATE)
        {
        }

        
        public override Type ToCSharpType()
        {
            return typeof(Date);
        }


        
        public override bool isAssignableFrom(Context context, IType other)
        {
			return base.isAssignableFrom(context, other) || other==DateTimeType.Instance;
        }

        
		public override IType checkAdd(Context context, IType other, bool tryReverse)
        {
            if (other is PeriodType)
                return this; // ignore time section
			return base.checkAdd(context, other, tryReverse);
        }

        
        public override IType checkSubstract(Context context, IType other)
        {
            if (other is PeriodType)
                return this; // ignore time section
            else if (other is DateType)
                return PeriodType.Instance;
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

        
        public override IType checkRange(Context context, IType other)
        {
            if (other is DateType)
                return new RangeType(this);
            return base.checkRange(context, other);
        }


		public  override IType checkMember(Context context, String name)
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
                return base.checkMember(context, name);
        }

        
        public override IRange newRange(Object left, Object right)
        {
            if (left is Date && right is Date)
                return new DateRange((Date)left, (Date)right);
            return base.newRange(left, right);
        } 

        
        public override String ToString(Object value)
        {
            return "'" + value.ToString() + "'";
        }

		public override Comparer<IValue> getNativeComparer(bool descending)
		{
			return new DateComparer(descending);
		}
    }

    class DateComparer : NativeComparer<Date>
    {
        public DateComparer(bool descending)
            : base(descending)
        {
        }
        
        protected override int DoCompare(Date o1, Date o2)
        {
            return o1.getMillis().CompareTo(o2.getMillis());
        }
    }

}
