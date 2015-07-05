using prompto.value;
using System;
using prompto.error;
using prompto.type;

namespace prompto.value
{

    public class TimeRange : Range<Time>
    {

        public TimeRange(Time left, Time right)
            : base(TimeType.Instance, left, right)
        {
        }

        override
        public Integer size()
        {
            TimeSpan interval = high.Value.Subtract(low.Value);
            return new Integer(1L + (long)interval.TotalSeconds);
        }

        override
        public int compare(Time o1, Time o2)
        {
            return o1.CompareTo(o2);
        }

        override
		public IValue Item(Integer index)
        {
            Time result = low.plusSeconds(index.IntegerValue - 1);
            if (result.isAfter(high))
                throw new IndexOutOfRangeError();
            return result;
        }

        override
        public Range<Time> newInstance(Time left, Time right)
        {
            return new TimeRange(left, right);
        }
    }
}
