using System;
using presto.parser;
using presto.type;
using presto.utils;
using presto.value;
using presto.grammar;


namespace presto.expression
{

    public abstract class SelectorExpression : IExpression
    {

        protected IExpression parent;

        public SelectorExpression()
        {
        }

        public SelectorExpression(IExpression parent)
        {
            this.parent = parent;
        }

        public IExpression getParent()
        {
            return parent;
        }

        public void setParent(IExpression parent)
        {
            this.parent = parent;
        }

        public abstract IType check(runtime.Context context);
  
		public abstract IValue interpret(runtime.Context context);

        public abstract void ToDialect(CodeWriter writer);

		public IType checkParent(runtime.Context context) {
			if (parent is UnresolvedIdentifier)
				return ((UnresolvedIdentifier)parent).checkMember(context);
			else
				return parent.check(context);
		}
	}

}
