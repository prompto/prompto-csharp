using System;
using prompto.runtime;
using prompto.value;
using System.Collections.Generic;
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
            : base("Character")
        {
        }

        
        public override Type ToCSharpType()
        {
            return typeof(char?);
        }


        override
        public bool isAssignableTo(Context context, IType other)
        {
            return (other is CharacterType) || (other is TextType) || (other is AnyType);
        }

		public override IType checkMember(Context context, String name)
		{
			if ("codePoint" == name)
				return IntegerType.Instance;
			else
				return base.checkMember(context, name);
		}

		override
		public IType checkAdd(Context context, IType other, bool tryReverse)
        {
            return TextType.Instance;
        }

        override
		public IType checkMultiply(Context context, IType other, bool tryReverse)
        {
            if (other is IntegerType)
                return TextType.Instance;
            else
				return base.checkMultiply(context, other, tryReverse);
        }

        override
        public IType checkCompare(Context context, IType other)
        {
            if (other is CharacterType || other is TextType)
                return BooleanType.Instance;
            return base.checkCompare(context, other);
        }

        override
        public IType checkRange(Context context, IType other)
        {
            if (other is CharacterType)
                return new RangeType(this);
            return base.checkRange(context, other);
        }

        override
        public IRange newRange(Object left, Object right)
        {
            if (left is Character && right is Character)
                return new CharacterRange((Character)left, (Character)right);
            return base.newRange(left, right);
        }

        override
		public ListValue sort(Context context, IContainer list)
        {
			return this.doSort(context, list, new CharacterComparer(context));
        }

        override
        public String ToString(Object value)
        {
            return "'" + value.ToString() + "'";
        }

        override
        public IValue ConvertCSharpValueToPromptoValue(Object value)
        {
            if (value is char)
                return new Character((char)value);
            else if (value is char?)
                return new Character(((char?)value).Value);
            else
                return (IValue)value; // TODO for now
        }

    }

    class CharacterComparer : ExpressionComparer<Character>
    {
       public CharacterComparer(Context context)
            : base(context)
        {
        }
 
        override
        protected int DoCompare(Character o1, Character o2)
        {
            return o1.Value.CompareTo(o2.Value);
        }
    }

}
