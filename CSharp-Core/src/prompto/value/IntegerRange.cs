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

        
        public override long Length()
        {
            return 1 + high.LongValue - low.LongValue;
        }

        
        public override int compare(IntegerValue o1, IntegerValue o2)
        {
            return o1.CompareTo(o2);
        }

        
		public override IValue Item(IntegerValue index)
        {
            Int64 result = low.LongValue + index.LongValue - 1;
            if (result > high.LongValue)
                throw new IndexOutOfRangeError();
            return new IntegerValue(result);
        }

        
        public override Range<IntegerValue> newInstance(IntegerValue left, IntegerValue right)
        {
            return new IntegerRange(left, right);
        }

    }

}
