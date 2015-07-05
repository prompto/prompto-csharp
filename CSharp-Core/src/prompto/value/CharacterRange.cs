using prompto.error;
using System;
using prompto.value;
using prompto.type;

namespace prompto.value
{

    public class CharacterRange : Range<Character>
    {

        public CharacterRange(Character left, Character right)
			:  base(CharacterType.Instance, left, right)
        {
        }

        override public Integer size()
        {
            return new Integer(1 + high.Value - low.Value);
        }

        override public int compare(Character o1, Character o2)
        {
            return o1.Value.CompareTo(o2.Value);
        }

		override public IValue Item(Integer index)
        {
            Char result = (char)(low.Value + index.IntegerValue - 1);
            if (result > high.Value)
                throw new IndexOutOfRangeError();
            return new Character(result);
        }

        override public Range<Character> newInstance(Character left, Character right)
        {
            return new CharacterRange(left, right);
        }

    }
}
