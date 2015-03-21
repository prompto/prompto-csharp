using System;
using presto.value;
using presto.error;
using presto.runtime;
using presto.grammar;
using presto.utils;
using presto.type;
using System.Collections.Generic;


namespace presto.value
{

    public class TupleValue : BaseValueList<TupleValue>, ISliceable, IMultiplyable
    {

        public TupleValue()
			: base(TupleType.Instance)
        {
        }

        public TupleValue(IValue item)
			: base(TupleType.Instance, item)
        {
        }

		public TupleValue(List<IValue> items)
			: base(TupleType.Instance, items)
		{
		}

		override
        public IValue Add(Context context, IValue value)
        {
            if (value is TupleValue)
                return this.merge((TupleValue)value);
            else if (value is ListValue)
                return this.merge((ListValue)value);
            else
                throw new SyntaxError("Illegal: Tuple + " + value.GetType().Name);
        }

        override
        public Int32 CompareTo(Context context, IValue value)
        {
            throw new NotSupportedException("Compare not supported by " + this.GetType().Name);
        }
			
        override
        public String ToString()
        {
            String result = base.ToString();
            return "(" + result + ")";
        }

		public void ToDialect(CodeWriter writer) {
			writer.append('(');
			if(this.Count>0) {
				foreach(Object o in this) {
					if(o is IDialectElement)
						((IDialectElement)o).ToDialect(writer);
					else
						writer.append(o.ToString());
					writer.append(", ");
				}
				writer.trimLast(2);
			}
			writer.append(')');
		}

        override
        protected TupleValue newInstance()
        {
            return new TupleValue();
        }

    }

}
