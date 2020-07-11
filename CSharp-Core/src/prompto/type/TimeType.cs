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
            return typeof(TimeValue);
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
        
         
        public override bool isAssignableFrom(Context context, IType other)
        {
			return base.isAssignableFrom(context, other)
				       || other==DateTimeType.Instance;
        }

         
		public override IType checkAdd(Context context, IType other, bool tryReverse)
        {
            if (other is PeriodType)
                return DateTimeType.Instance;
			return base.checkAdd(context, other, tryReverse);
        }

         
        public override IType checkSubstract(Context context, IType other)
        {
            if (other is TimeType)
                return PeriodType.Instance;
            if (other is PeriodType)
                return this;
            return base.checkSubstract(context, other);
        }

         
        public override void checkCompare(Context context, IType other)
        {
            if (other is TimeType)
                return;
            else
                base.checkCompare(context, other);
        }

        
        public override IType checkRange(Context context, IType other)
        {
            if (other is TimeType)
                return new RangeType(this);
            return base.checkRange(context, other);
        }

        
        public override IRange newRange(Object left, Object right)
        {
            if (left is TimeValue && right is TimeValue)
                return new TimeRange((TimeValue)left, (TimeValue)right);
            return base.newRange(left, right);
        }

         
        public override String ToString(Object value)
        {
            return "'" + value.ToString() + "'";
        }

		public override Comparer<IValue> getNativeComparer(bool descending)
		{
			return new TimeComparer(descending);
		}

    }

    class TimeComparer : NativeComparer<TimeValue>
    {
        public TimeComparer(bool descending)
            : base(descending)
        {
        }

        
        protected override int DoCompare(TimeValue o1, TimeValue o2)
        {
            return o1.CompareTo(o2);
        }
    }

}