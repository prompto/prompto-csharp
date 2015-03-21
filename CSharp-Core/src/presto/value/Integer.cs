using System;
using presto.error;
using presto.runtime;
using presto.grammar;
using presto.type;


namespace presto.value
{
    public class Integer : BaseValue, INumber, IComparable<INumber>, IMultiplyable
    {
        public static Integer Parse(String text)
        {
            return new Integer(Int64.Parse(text));
        }

        Int64 value;

        public Integer(Int64 value)
			: base(IntegerType.Instance)
        {
            this.value = value;
        }

        public Int64 IntegerValue { get { return value; } }

        public Double DecimalValue { get { return value; } }

        override
        public IValue Add(Context context, IValue value)
        {
            if (value is Integer)
                return new Integer(this.IntegerValue + ((Integer)value).IntegerValue);
            else if (value is Decimal)
                return new Decimal(((Decimal)value).DecimalValue + this.value);
            else
                throw new SyntaxError("Illegal: Integer + " + value.GetType().Name);
        }

        override
        public IValue Subtract(Context context, IValue value)
        {
            if (value is Integer)
                return new Integer(this.IntegerValue - ((Integer)value).IntegerValue);
            else if (value is Decimal)
                return new Decimal(this.DecimalValue - ((Decimal)value).DecimalValue);
            else
                throw new SyntaxError("Illegal: Integer - " + value.GetType().Name);
        }

        override
        public IValue Multiply(Context context, IValue value)
        {
            if (value is Integer)
                return new Integer(this.IntegerValue * ((Integer)value).IntegerValue);
            else if (value is Decimal)
                return new Decimal(((Decimal)value).DecimalValue * this.IntegerValue);
            else if (value is IMultiplyable)
                return value.Multiply(context, this);
            else
                throw new SyntaxError("Illegal: Integer * " + value.GetType().Name);
        }

        override
        public IValue Divide(Context context, IValue value)
        {
            if (value is INumber)
            {
                if(((INumber)value).DecimalValue==0.0)
                    throw new DivideByZeroError();
                else
                    return new Decimal(this.DecimalValue / ((INumber)value).DecimalValue);
             }
            else
                throw new SyntaxError("Illegal: Integer / " + value.GetType().Name);
        }

        override
        public IValue IntDivide(Context context, IValue value)
        {
            if (value is Integer)
            {
                if (((Integer)value).IntegerValue == 0)
                    throw new DivideByZeroError();
                else
                    return new Integer(this.IntegerValue / ((Integer)value).IntegerValue);
            }
            else
                throw new SyntaxError("Illegal: Integer \\ " + value.GetType().Name);
        }

        override
        public IValue Modulo(Context context, IValue value)
        {
            if (value is Integer)
            {
                try
                {
                    return new Integer(this.IntegerValue % ((Integer)value).IntegerValue);
                }
                catch (DivideByZeroException)
                {
                    throw new DivideByZeroError();
                }
            }
            else
                throw new SyntaxError("Illegal: Integer % " + value.GetType().Name);
        }
        
        public int CompareTo(INumber obj)
        {
            return value.CompareTo(obj.IntegerValue);
        }

        override
        public Int32 CompareTo(Context context, IValue value)
        {
            if (value is Integer)
                return this.value.CompareTo(((Integer)value).IntegerValue);
            else if (value is Decimal)
                return this.DecimalValue.CompareTo(((Decimal)value).DecimalValue);
            else
                throw new SyntaxError("Illegal comparison: Integer and " + value.GetType().Name);

        }

        override
        public Object ConvertTo(Type type)
        {
            return value;
        }
 
        override
        public String ToString()
        {
            return value.ToString();
        }

        
		public override bool Equals(object obj)
        {
			if (obj is Decimal)
				return value == ((Decimal)obj).DecimalValue;
            else if (obj is Integer)
				return value == ((Integer)obj).value;
            else
                return value.Equals(obj);
	    }

        
		public override int GetHashCode()
        {
            return value.GetHashCode();
        }
    }
}
