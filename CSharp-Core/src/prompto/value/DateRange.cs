using prompto.value;
using System;
using prompto.error;
using prompto.type;


namespace prompto.value
{

    public class DateRange : Range<Date>
    {

        public DateRange(Date left, Date right)
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
        public int compare(Date o1, Date o2)
        {
            return o1.CompareTo(o2);
        }

        override
		public IValue Item(Integer index)
        {
            Date result = low.plusDays(index.IntegerValue - 1);
            if (result.isAfter(high))
                throw new IndexOutOfRangeError();
            return result;
        }

        override
        public Range<Date> newInstance(Date left, Date right)
        {
            return new DateRange(left, right);
        }

    }
}
