using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using prompto.error;
using prompto.runtime;
using prompto.type;

namespace prompto.value
{
    public class Decimal : BaseValue, INumber, IComparable<INumber>, IMultiplyable
    {
        public static Decimal Parse(String text)
        {
            return new Decimal(Double.Parse(text, CultureInfo.InvariantCulture));
        }

        Double value;

        public Decimal(Double value)
			: base(DecimalType.Instance)
        {
            this.value = value;
        }

        public Int64 IntegerValue { get { return (Int64)value; } }
        
        public Double DecimalValue { get { return value; } }

        override
        public IValue Add(Context context, IValue value)
        {
            if (value is Integer)
                return new Decimal(this.value + ((Integer)value).IntegerValue);
            else if (value is Decimal)
                return new Decimal(this.value + ((Decimal)value).DecimalValue);
            else
                throw new SyntaxError("Illegal: Decimal + " + value.GetType().Name);
        }

        override
        public IValue Subtract(Context context, IValue value)
        {
            if (value is Integer)
                return new Decimal(this.value - ((Integer)value).IntegerValue);
            else if (value is Decimal)
                return new Decimal(this.value - ((Decimal)value).DecimalValue);
            else
                throw new SyntaxError("Illegal: Decimal - " + value.GetType().Name);
        }

        override
        public IValue Multiply(Context context, IValue value)
        {
             if (value is Integer)
                 return new Decimal(this.DecimalValue * ((Integer)value).IntegerValue);
            else if (value is Decimal)
                 return new Decimal(this.DecimalValue * ((Decimal)value).DecimalValue);
            else
                throw new SyntaxError("Illegal: Decimal * " + value.GetType().Name);
       }

        override
        public IValue Divide(Context context, IValue value)
        {
            if (value is INumber)
            {
                if(((INumber)value).DecimalValue == 0.0)
                    throw new DivideByZeroError();
                else
                    return new Decimal(this.DecimalValue / ((INumber)value).DecimalValue);
             }
            else
                throw new SyntaxError("Illegal: Decimal / " + value.GetType().Name);
        }

		public override IValue IntDivide(Context context, IValue value) {
			if (value is Integer) {
				if (((Integer) value).IntegerValue == 0)
					throw new DivideByZeroError();
				else
					return new Integer(this.IntegerValue / ((Integer) value).IntegerValue);
			} else
				throw new SyntaxError("Illegal: Decimal \\ " + value.GetType().Name);
		}

		public override IValue Modulo(Context context, IValue value) {
			if (value is INumber) {
				if (((INumber) value).DecimalValue == 0.0)
					throw new DivideByZeroError();
				else
					return new Decimal(this.DecimalValue % ((INumber) value).DecimalValue);
			} else
				throw new SyntaxError("Illegal: Decimal % " + value.GetType().Name);
		}

        public int CompareTo(INumber obj)
        {
            return value.CompareTo(obj.DecimalValue);
        }

        override
        public Int32 CompareTo(Context context, IValue value)
        {
            if (value is INumber)
                return this.value.CompareTo(((INumber)value).DecimalValue);
            else
                throw new SyntaxError("Illegal comparison: Decimal and " + value.GetType().Name);

        }

        override
        public Object ConvertTo(Type type)
        {
            return value;
        }
 
        override
        public String ToString()
        {
            return value.ToString("0.0######", CultureInfo.InvariantCulture);
        }

         override
        public bool Equals(object obj)
        {
			if (obj is Decimal)
				return value == ((Decimal)obj).value;
			else if (obj is Integer)
				return value == ((Integer)obj).IntegerValue;
			else
				return value.Equals(obj);
        }

        override
        public int GetHashCode()
        {
            return value.GetHashCode();
        }

    }
}
