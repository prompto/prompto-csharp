using System.Collections.Generic;
using prompto.value;

namespace prompto.runtime
{
	public abstract class NativeComparer<T> : Comparer<IValue> where T : IValue
    {
     	protected bool descending;

        protected NativeComparer(bool descending)
        {
  			this.descending = descending;
        }

        public override int Compare(IValue o1, IValue o2)
        {
			return descending ? DoCompare((T)o2, (T)o1) : DoCompare((T)o1, (T)o2);
        }

        protected abstract int DoCompare(T o1, T o2);
    }
}
