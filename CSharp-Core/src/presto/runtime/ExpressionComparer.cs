using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using presto.grammar;
using presto.expression;

namespace presto.runtime
{
    public abstract class ExpressionComparer<T> : Comparer<Object> 
    {
        Context context;

        protected ExpressionComparer(Context context)
        {
            this.context = context;
        }

        override
        public int Compare(Object o1, Object o2)
        {
           T t1 = Evaluate(o1);
           T t2 = Evaluate(o2);
           return DoCompare(t1,t2);
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
