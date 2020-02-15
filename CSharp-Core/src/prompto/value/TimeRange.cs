using prompto.value;
using System;
using prompto.error;
using prompto.type;

namespace prompto.value
{

    public class TimeRange : Range<TimeValue>
    {

        public TimeRange(TimeValue left, TimeValue right)
            : base(TimeType.Instance, left, right)
        {
        }

        override
        public long Length()
        {
            TimeSpan interval = high.Value.Subtract(low.Value);
            return 1L + (long)interval.TotalSeconds;
        }

        override
        public int compare(TimeValue o1, TimeValue o2)
        {
            return o1.CompareTo(o2);
        }

        override
		public IValue Item(IntegerValue index)
        {
            TimeValue result = low.plusSeconds(index.LongValue - 1);
            if (result.isAfter(high))
                throw new IndexOutOfRangeError();
            return result;
        }

        override
        public Range<TimeValue> newInstance(TimeValue left, TimeValue right)
        {
            return new TimeRange(left, right);
        }
    }
}
