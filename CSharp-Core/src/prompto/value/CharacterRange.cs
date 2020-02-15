using prompto.error;
using System;
using prompto.value;
using prompto.type;

namespace prompto.value
{

    public class CharacterRange : Range<CharacterValue>
    {

        public CharacterRange(CharacterValue left, CharacterValue right)
			:  base(CharacterType.Instance, left, right)
        {
        }

        override public long Length()
        {
            return 1 + high.Value - low.Value;
        }

        override public int compare(CharacterValue o1, CharacterValue o2)
        {
            return o1.Value.CompareTo(o2.Value);
        }

		override public IValue Item(IntegerValue index)
        {
            Char result = (char)(low.Value + index.LongValue - 1);
            if (result > high.Value)
                throw new IndexOutOfRangeError();
            return new CharacterValue(result);
        }

        override public Range<CharacterValue> newInstance(CharacterValue left, CharacterValue right)
        {
            return new CharacterRange(left, right);
        }

    }
}
