using System.Collections.Generic;
using prompto.value;

namespace prompto.runtime
{
    public abstract class ValueComparer<T> : Comparer<IValue> where T : IValue
    {
        protected Context context;
		protected bool descending;

        protected ValueComparer(Context context, bool descending)
        {
            this.context = context;
			this.descending = descending;
        }

        
        public override int Compare(IValue o1, IValue o2)
        {
			return descending ? DoCompare((T)o2, (T)o1) : DoCompare((T)o1, (T)o2);
        }

        protected abstract int DoCompare(T o1, T o2);
    }
}
