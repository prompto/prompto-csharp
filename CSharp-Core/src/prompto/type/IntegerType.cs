using System;
using prompto.runtime;
using prompto.value;
using System.Collections.Generic;
using prompto.store;
using prompto.declaration;
using prompto.param;
using prompto.literal;

namespace prompto.type
{

    public class IntegerType : NativeType
    {

        static IntegerType instance_ = new IntegerType();

        public static IntegerType Instance
        {
            get
            {
                return instance_;
            }
        }

        private IntegerType()
            : base(TypeFamily.INTEGER)
        {
        }


        public override Type ToCSharpType()
        {
            return typeof(Int64);
        }


        public override bool isAssignableFrom(Context context, IType other)
        {
            return base.isAssignableFrom(context, other) || other == DecimalType.Instance;
        }


        public override IType checkAdd(Context context, IType other, bool tryReverse)
        {
            if (other is IntegerType)
                return this;
            if (other is DecimalType)
                return other;
            return base.checkAdd(context, other, tryReverse);
        }

        override
        public IType checkSubstract(Context context, IType other)
        {
            if (other is IntegerType)
                return this;
            if (other is DecimalType)
                return other;
            return base.checkSubstract(context, other);
        }


        public override IType checkMultiply(Context context, IType other, bool tryReverse)
        {
            if (other is IntegerType)
                return this;
            if (other is DecimalType)
                return other;
            if (other is CharacterType)
                return TextType.Instance;
            if (other is TextType)
                return other;
            if (other is PeriodType)
                return other;
            if (other is ListType)
                return other;
            return base.checkMultiply(context, other, tryReverse);
        }

        override
        public IType checkDivide(Context context, IType other)
        {
            if (other is IntegerType)
                return DecimalType.Instance;
            if (other is DecimalType)
                return other;
            return base.checkDivide(context, other);
        }

        override
       public IType checkIntDivide(Context context, IType other)
        {
            if (other is IntegerType)
                return IntegerType.Instance;
            return base.checkDivide(context, other);
        }


        public override IType checkModulo(Context context, IType other)
        {
            if (other is IntegerType)
                return IntegerType.Instance;
            return base.checkDivide(context, other);
        }

        override
        public IType checkCompare(Context context, IType other)
        {
            if (other is IntegerType)
                return BooleanType.Instance;
            if (other is DecimalType)
                return BooleanType.Instance;
            return base.checkCompare(context, other);
        }

        override
        public IType checkRange(Context context, IType other)
        {
            if (other is IntegerType)
                return new RangeType(this);
            return base.checkRange(context, other);
        }


        public override IRange newRange(Object left, Object right)
        {
            if (left is Integer && right is Integer)
                return new IntegerRange((Integer)left, (Integer)right);
            return base.newRange(left, right);
        }

        public override IValue ConvertCSharpValueToIValue(Context context, Object value)
        {
            if (value is Int16)
                return new Integer((Int16)value);
            else if (value is Int16?)
                return new Integer(((Int16?)value).Value);
            else if (value is Int32)
                return new Integer((Int32)value);
            else if (value is Int32?)
                return new Integer(((Int32?)value).Value);
            else if (value is Int64)
                return new Integer((Int64)value);
            else if (value is Int64?)
                return new Integer(((Int64?)value).Value);
            else
                return (IValue)value; // TODO for now
        }


        public override IType checkStaticMember(Context context, String name)
        {
            if (name == "min")
                return this;
            else if (name == "max")
                return this;
            else
                return base.checkStaticMember(context, name);
        }


        public override IValue getStaticMemberValue(Context context, String name)
        {
            if (name == "min")
                return new Integer(Int64.MinValue);
            else if (name == "max")
                return new Integer(Int64.MaxValue);
            else
                return base.getStaticMemberValue(context, name);
        }

        public override ISet<IMethodDeclaration> getMemberMethods(Context context, string name)
        {
            ISet<IMethodDeclaration> list = new HashSet<IMethodDeclaration>();
            switch (name)
            {
                case "format":
                    list.Add(FORMAT_METHOD);
                    return list;
                default:
                    return base.getMemberMethods(context, name);
            }
        }

        public override Comparer<IValue> getNativeComparer(bool descending)
        {
            return new IntegerComparer(descending);
        }

        internal static IParameter FORMAT_ARGUMENT = new CategoryParameter(TextType.Instance, "format", new TextLiteral("\"format\""));
        internal static IMethodDeclaration FORMAT_METHOD = new FormatMethodDeclaration();
    }

    class FormatMethodDeclaration : BuiltInMethodDeclaration
    {

        public FormatMethodDeclaration()
            : base("format", IntegerType.FORMAT_ARGUMENT)
        { }

        public override IValue interpret(Context context)
        {
            Int64 value = (Int64)getValue(context).GetStorableData();
            string format = (String)context.getValue("format").GetStorableData();
            string result = value.ToString(format);
            return new Text(result);
        }



        public override IType check(Context context)
        {
            return TextType.Instance;
        }



    };


    class IntegerComparer : NativeComparer<INumber>
    {

        public IntegerComparer(bool descending)
            : base(descending)
        {
        }


        protected override int DoCompare(INumber o1, INumber o2)
        {
            return o1.IntegerValue.CompareTo(o2.IntegerValue);
        }

    }

}
