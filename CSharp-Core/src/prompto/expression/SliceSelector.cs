using prompto.runtime;
using System;
using prompto.error;
using prompto.value;
using prompto.parser;
using prompto.type;
using prompto.grammar;
using prompto.utils;

namespace prompto.expression
{

    public class SliceSelector : SelectorExpression
    {

        IExpression first;
        IExpression last;

        public SliceSelector(IExpression first, IExpression last)
        {
            this.first = first;
            this.last = last;
        }

        public SliceSelector(IExpression parent, IExpression first, IExpression last)
            : base(parent)
        {
            this.first = first;
            this.last = last;
        }

        public IExpression getFirst()
        {
            return first;
        }

        public IExpression getLast()
        {
            return last;
        }

        override
        public void ToDialect(CodeWriter writer)
        {
			parent.ToDialect(writer);
			writer.append('[');
			if(first!=null)
				first.ToDialect(writer);
			writer.append(':');
			if(last!=null)
				last.ToDialect(writer);
			writer.append(']');
		}

        override
        public IType check(Context context)
        {
            IType firstType = first != null ? first.check(context) : null;
            IType lastType = last != null ? last.check(context) : null;
            if (firstType != null && !(firstType is IntegerType))
                throw new SyntaxError(firstType.ToString() + " is not an integer");
            if (lastType != null && !(lastType is IntegerType))
                throw new SyntaxError(lastType.ToString() + " is not an integer");
            IType parentType = parent.check(context);
            return parentType.checkSlice(context);
        }

        override
		public IValue interpret(Context context)
        {
			IValue o = parent.interpret(context);
            if (o == null)
                throw new NullReferenceError();
            if (o is ISliceable)
            {
				IValue fi = first != null ? first.interpret(context) : null;
                if (fi != null && !(fi is IntegerValue))
                    throw new SyntaxError("Illegal sliced type: " + fi);
				IValue li = last != null ? last.interpret(context) : null;
                if (li != null && !(li is IntegerValue))
                    throw new SyntaxError("Illegal sliced type: " + li);
                return ((ISliceable)o).Slice(context, (IntegerValue)fi, (IntegerValue)li);
            }
            else
                throw new SyntaxError("Illegal sliced object: " + parent);
        }

      }

}