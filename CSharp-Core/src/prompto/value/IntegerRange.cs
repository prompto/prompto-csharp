using prompto.error;
using System;
using prompto.value;
using prompto.type;

namespace prompto.value
{

    public class IntegerRange : Range<IntegerValue>
    {

        public IntegerRange(IntegerValue left, IntegerValue right)
			: base(IntegerType.Instance, left, right)
        {
        }

        override
        public long Length()
        {
            return 1 + high.LongValue - low.LongValue;
        }

        override
        public int compare(IntegerValue o1, IntegerValue o2)
        {
            return o1.CompareTo(o2);
        }

        override
		public IValue Item(IntegerValue index)
        {
            Int64 result = low.LongValue + index.LongValue - 1;
            if (result > high.LongValue)
                throw new IndexOutOfRangeError();
            return new IntegerValue(result);
        }

        override
        public Range<IntegerValue> newInstance(IntegerValue left, IntegerValue right)
        {
            return new IntegerRange(left, right);
        }

    }

}
