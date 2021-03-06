using System;
using prompto.runtime;
using prompto.value;
using System.Collections.Generic;
using prompto.store;

namespace prompto.type
{

    public class CharacterType : NativeType
    {

        static CharacterType instance_ = new CharacterType();

      
        public static CharacterType Instance
        {
            get
            {
                return instance_;
            }
        }

        private CharacterType()
			: base(TypeFamily.CHARACTER)
        {
        }

        
        public override Type ToCSharpType(Context context)
        {
            return typeof(char?);
        }


        
        public override bool isAssignableFrom(Context context, IType other)
        {
            return other is CharacterType;
        }

		public override IType checkMember(Context context, String name)
		{
			if ("codePoint" == name)
				return IntegerType.Instance;
			else
				return base.checkMember(context, name);
		}

		
		public override IType checkAdd(Context context, IType other, bool tryReverse)
        {
            return TextType.Instance;
        }

        
		public override IType checkMultiply(Context context, IType other, bool tryReverse)
        {
            if (other is IntegerType)
                return TextType.Instance;
            else
				return base.checkMultiply(context, other, tryReverse);
        }

        
        public override void checkCompare(Context context, IType other)
        {
            if (other is CharacterType || other is TextType)
                return;
            else
                base.checkCompare(context, other);
        }

        
        public override IType checkRange(Context context, IType other)
        {
            if (other is CharacterType)
                return new RangeType(this);
            return base.checkRange(context, other);
        }

        
        public override IRange newRange(Object left, Object right)
        {
            if (left is CharacterValue && right is CharacterValue)
                return new CharacterRange((CharacterValue)left, (CharacterValue)right);
            return base.newRange(left, right);
        }

        
        public override String ToString(Object value)
        {
            return "'" + value.ToString() + "'";
        }

        
        public override IValue ConvertCSharpValueToIValue(Context context, Object value)
        {
            if (value is char)
                return new CharacterValue((char)value);
            else if (value is char?)
                return new CharacterValue(((char?)value).Value);
            else
                return (IValue)value; // TODO for now
        }

    }

    class CharacterComparer : ValueComparer<CharacterValue>
    {
       public CharacterComparer(Context context, bool descending)
            : base(context, descending)
        {
        }
 
        
        protected override int DoCompare(CharacterValue o1, CharacterValue o2)
        {
            return o1.Value.CompareTo(o2.Value);
        }
    }

}
