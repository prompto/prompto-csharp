using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using prompto.grammar;
using prompto.expression;

namespace prompto.runtime
{
    public abstract class ExpressionComparer<T> : Comparer<Object> 
    {
        protected Context context;
		protected bool descending;

        protected ExpressionComparer(Context context, bool descending)
        {
            this.context = context;
			this.descending = descending;
        }

        override
        public int Compare(Object o1, Object o2)
        {
           T t1 = Evaluate(o1);
           T t2 = Evaluate(o2);
           return descending ? DoCompare(t2, t1) : DoCompare(t1,t2);
        }

        private T Evaluate(Object o)
        {
            if (o is IExpression)
                o = ((IExpression)o).interpret(context);
            return (T)o;
        }

        protected abstract int DoCompare(T o1, T o2);
    }
}
