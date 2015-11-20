using prompto.error;
using System;
using prompto.value;
using prompto.type;

namespace prompto.value
{

    public class IntegerRange : Range<Integer>
    {

        public IntegerRange(Integer left, Integer right)
			: base(IntegerType.Instance, left, right)
        {
        }

        override
        public long Length()
        {
            return 1 + high.IntegerValue - low.IntegerValue;
        }

        override
        public int compare(Integer o1, Integer o2)
        {
            return o1.CompareTo(o2);
        }

        override
		public IValue Item(Integer index)
        {
            Int64 result = low.IntegerValue + index.IntegerValue - 1;
            if (result > high.IntegerValue)
                throw new IndexOutOfRangeError();
            return new Integer(result);
        }

        override
        public Range<Integer> newInstance(Integer left, Integer right)
        {
            return new IntegerRange(left, right);
        }

    }

}
