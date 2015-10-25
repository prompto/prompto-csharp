using System;
using prompto.value;
using prompto.error;
using prompto.runtime;
using prompto.grammar;
using prompto.utils;
using prompto.type;
using System.Collections.Generic;


namespace prompto.value
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

	 override
	public Int32 CompareTo(Context context, IValue value)
	{
			if(!(value is TupleValue))
				return base.CompareTo(context, value);
			return CompareTo(context, (TupleValue)value, new List<bool>());
		}

		public Int32 CompareTo(Context context, TupleValue other, List<bool> directions) {
			IEnumerator<bool> iterDirs = directions.GetEnumerator();
			IEnumerator<IValue> iterThis = this.GetEnumerator();
			IEnumerator<IValue> iterOther = other.GetEnumerator();
			while(iterThis.MoveNext()) {
				bool descending = iterDirs.MoveNext() ? iterDirs.Current : false;
				if(iterOther.MoveNext()) {
					// compare items
					IValue thisVal = iterThis.Current;
					IValue otherVal = iterOther.Current;
					if(thisVal==null && otherVal==null)
						continue;
					else if(thisVal==null)
						return descending ? 1 : -1;
					else if(otherVal==null)
						return descending ? -1 : 1;
					int cmp = thisVal.CompareTo(context, otherVal);
					// if not equal, done
					if(cmp!=0)
						return descending ? -cmp : cmp;
				} else
					return descending ? -1 : 1;
			}
			bool desc2 = iterDirs.MoveNext() ? iterDirs.Current : false;
			if(iterOther.MoveNext())
				return desc2 ? 1 : -1;
			else
				return 0;
		}	
	}

}
