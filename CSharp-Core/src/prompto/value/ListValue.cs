using System;
using prompto.value;
using prompto.error;
using prompto.runtime;
using prompto.grammar;
using System.Collections.Generic;
using prompto.expression;
using prompto.type;


namespace prompto.value
{


    public class ListValue : BaseValueList<ListValue>, ISliceable, IMultiplyable
    {

        public ListValue(IType itemType)
			: base(new ListType(itemType))
        {
        }

		public ListValue(IType itemType, IValue value)
			: base(new ListType(itemType), value)
        {
        }

		public ListValue(IType itemType, List<IValue> values)
			: base(new ListType(itemType), values)
		{
		}

		public ListValue(IType itemType, List<IValue> values, bool mutable)
			: base(new ListType(itemType), values, mutable)
		{
		}

		      
		public override IValue Add(Context context, IValue value)
        {
			if (value is ListValue)
                return this.merge((ListValue)value);
			else if (value is SetValue)
				return this.merge((SetValue)value);
			else
                throw new SyntaxError("Illegal : List + " + value.GetType().Name);
        }

        override
        public IValue Multiply(Context context, IValue value)
        {
            if (value is Integer)
            {
                int count = (int)((Integer)value).IntegerValue;
                if (count < 0)
                    throw new SyntaxError("Negative repeat count:" + count);
                if (count == 0)
					return new ListValue(this.ItemType);
                if (count == 1)
                    return this;
				ListValue result = new ListValue(this.ItemType);
                for (long i = 0; i < count; i++)
                    result.AddRange(this); // TODO: interpret items ?
                return result;
            }
            else
                throw new SyntaxError("Illegal: List * " + value.GetType().Name);
       }

        override
        public String ToString()
        {
            String result = base.ToString();
            return "[" + result + "]";
        }
        
        override
        protected ListValue newInstance()
        {
			return new ListValue(this.ItemType);
        }

		public override bool Equals(Context context, IValue rval)
		{
			if (!(rval is ListValue))
				return false;
			ListValue list = (ListValue)rval;
			if (this.Count != list.Count)
				return false;
			IEnumerator<Object> li = this.GetEnumerator();
			IEnumerator<Object> ri = list.GetEnumerator();
			while (li.MoveNext() && ri.MoveNext())
			{
				Object lival = li.Current;
				if (lival is IExpression)
					lival = ((IExpression)lival).interpret(context);
				Object rival = ri.Current;
				if (rival is IExpression)
					rival = ((IExpression)rival).interpret(context);
				if (lival is IValue && rival is IValue) {
					if (!((IValue)lival).Equals (context, (IValue)rival))
						return false;
				} else if (!lival.Equals(rival))
					return false;
			}
			return true;
		}

    }
}
