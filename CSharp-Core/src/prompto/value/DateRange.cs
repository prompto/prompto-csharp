using prompto.value;
using System;
using prompto.error;
using prompto.type;


namespace prompto.value
{

    public class DateRange : Range<DateValue>
    {

        public DateRange(DateValue left, DateValue right)
            : base(DateType.Instance, left, right)
        {
        }

        override
        public long Length()
        {
            long h = high.getMillis();
            long l = low.getMillis();
            return 1 + ((h - l) / (24 * 60 * 60 * 1000));
        }

        override
        public int compare(DateValue o1, DateValue o2)
        {
            return o1.CompareTo(o2);
        }

        override
		public IValue Item(IntegerValue index)
        {
            DateValue result = low.plusDays(index.LongValue - 1);
            if (result.isAfter(high))
                throw new IndexOutOfRangeError();
            return result;
        }

        override
        public Range<DateValue> newInstance(DateValue left, DateValue right)
        {
            return new DateRange(left, right);
        }

    }
}
