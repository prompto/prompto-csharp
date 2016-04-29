

using prompto.runtime;
using System;
using System.Collections.Generic;
using prompto.value;

namespace prompto.type
{

    public class TextType : NativeType
    {

        static TextType instance_ = new TextType();


        public static TextType Instance
        {
            get
            {
                return instance_;
            }
        }

        private TextType()
            : base("Text")
        {
        }

        override
        public Type ToCSharpType()
        {
            return typeof(String);
        }

        
		public override IType checkMember(Context context, String name)
        {
            if ("length" == name)
                return IntegerType.Instance;
            else
                return base.checkMember(context, name);
        }

        override
        public bool isAssignableTo(Context context, IType other)
        {
            return (other is TextType) || (other is AnyType);
        }

        
		public override IType checkAdd(Context context, IType other, bool tryReverse)
        {
            return this;
        }

        
		public override IType checkMultiply(Context context, IType other, bool tryReverse)
        {
            if (other is IntegerType)
                return this;
            else
				return base.checkMultiply(context, other, tryReverse);
        }

        override
        public IType checkCompare(Context context, IType other)
        {
            if (other is TextType || other is CharacterType)
                return BooleanType.Instance;
            return base.checkCompare(context, other);
        }

        override
        public IType checkItem(Context context, IType other)
        {
            if (other == IntegerType.Instance)
                return CharacterType.Instance;
            else
                return base.checkItem(context, other);
        }

        override
        public IType checkContains(Context context, IType other)
        {
            if (other is TextType || other is CharacterType)
                return BooleanType.Instance;
            return base.checkContains(context, other);
        }

        override
        public IType checkContainsAllOrAny(Context context, IType other)
        {
            return BooleanType.Instance;
        }

        override
        public IType checkSlice(Context context)
        {
            return this;
        }

 
        override
		public ListValue sort(Context context, IContainer list)
        {
			return this.doSort(context, list, new StringComparer(context));
        }

        override
        public String ToString(Object value)
        {
            if (value is Char)
                return "'" + value.ToString() + "'";
            else
                return "" + '"' + value + '"';
        }

        override
        public IValue ConvertCSharpValueToPromptoValue(Object value)
        {
            if (value is String)
                return new Text((String)value);
            else
                return (IValue)value; // TODO for now
        }


    }

    class StringComparer : ExpressionComparer<Object>
    {
         public StringComparer(Context context)
             : base(context)
        {
         }

        override
        protected int DoCompare(Object o1, Object o2)
        {
           return o1.ToString().CompareTo(o2.ToString());
        }

     }


}