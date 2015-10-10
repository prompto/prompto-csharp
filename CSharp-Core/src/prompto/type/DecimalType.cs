using System;
using prompto.runtime;
using Decimal = prompto.value.Decimal;
using System.Collections.Generic;
using prompto.value;

namespace prompto.type
{

    public class DecimalType : NativeType
    {

        static DecimalType instance_ = new DecimalType();

       
        public static DecimalType Instance
        {
            get
            {
                return instance_;
            }
        }

        private DecimalType()
            : base("Decimal")
        {
        }

        override
        public Type ToCSharpType()
        {
            return typeof(Decimal);
        }


        override
        public bool isAssignableTo(Context context, IType other)
        {
            return (other is IntegerType) || (other is DecimalType) || (other is AnyType);
        }

        override
		public IType checkAdd(Context context, IType other, bool tryReverse)
        {
            if (other is IntegerType)
                return this;
            if (other is DecimalType)
                return this;
			return base.checkAdd(context, other, tryReverse);
        }

        override
        public IType checkSubstract(Context context, IType other)
        {
            if (other is IntegerType)
                return this;
            if (other is DecimalType)
                return this;
            return base.checkSubstract(context, other);
        }

        override
		public IType checkMultiply(Context context, IType other, bool tryReverse)
        {
            if (other is IntegerType)
                return this;
            if (other is DecimalType)
                return this;
			return base.checkMultiply(context, other, tryReverse);
        }

        override
        public IType checkDivide(Context context, IType other)
        {
            if (other is IntegerType)
                return this;
            if (other is DecimalType)
                return this;
            return base.checkDivide(context, other);
        }

		public override IType checkIntDivide(Context context, IType other) {
			if(other is IntegerType)
				return other;
			return base.checkIntDivide(context, other);
		}

		public override IType CheckModulo(Context context, IType other) {
			if(other is IntegerType)
				return this;
			if(other is DecimalType)
				return this;
			return base.CheckModulo(context, other);
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
		public ListValue sort(Context context, IContainer list)
        {
			return this.doSort(context, list, new DecimalComparer(context));
        }

        override
        public IValue ConvertCSharpValueToPromptoValue(Object value)
        {
            if (value is float)
                return new Decimal((float)value);
            else if (value is float?)
                return new Decimal(((float?)value).Value);
            else if (value is double)
                return new Decimal((double)value);
            else if (value is double?)
                return new Decimal(((double?)value).Value);
            else
                return (IValue)value; // TODO for now
        }
    }

    class DecimalComparer : ExpressionComparer<INumber>
    {
        public DecimalComparer(Context context)
            : base(context)
        {
        }

        override
        protected int DoCompare(INumber o1, INumber o2)
        {
            return o1.DecimalValue.CompareTo(o2.DecimalValue);
        }
    }

}
