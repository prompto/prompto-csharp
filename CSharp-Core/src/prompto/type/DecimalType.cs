using System;
using prompto.runtime;
using DecimalValue = prompto.value.DecimalValue;
using prompto.value;
using prompto.store;
using System.Collections.Generic;

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
			: base(TypeFamily.DECIMAL)
        {
        }

        
        public override Type ToCSharpType(Context context)
        {
			return typeof(Double);
        }


        
        public override bool isAssignableFrom(Context context, IType other)
        {
			return base.isAssignableFrom(context, other) || other == IntegerType.Instance;
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

		public override IType checkModulo(Context context, IType other) {
			if(other is IntegerType)
				return this;
			if(other is DecimalType)
				return this;
			return base.checkModulo(context, other);
		}

        
        public override void checkCompare(Context context, IType other)
        {
            if (other is IntegerType || other is DecimalType)
                return;
            else
                base.checkCompare(context, other);
        }

        
        public override IValue ConvertCSharpValueToIValue(Context context, Object value)
        {
            if (value is float)
                return new DecimalValue((float)value);
            else if (value is float?)
                return new DecimalValue(((float?)value).Value);
            else if (value is double)
                return new DecimalValue((double)value);
            else if (value is double?)
                return new DecimalValue(((double?)value).Value);
            else
                return (IValue)value; // TODO for now
        }

		public override Comparer<IValue> getNativeComparer(bool descending)
		{
			return new DecimalComparer(descending);
		}

	}

    class DecimalComparer : NativeComparer<INumber>
    {
        public DecimalComparer(bool descending)
            : base(descending)
        {
        }

        
        protected override int DoCompare(INumber o1, INumber o2)
        {
            return o1.DoubleValue.CompareTo(o2.DoubleValue);
        }
    }

}
