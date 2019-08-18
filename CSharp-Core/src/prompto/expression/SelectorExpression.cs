using System;
using prompto.parser;
using prompto.type;
using prompto.utils;
using prompto.value;
using prompto.grammar;


namespace prompto.expression
{

    public abstract class SelectorExpression : BaseExpression, IExpression
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

		public IType checkParent(runtime.Context context) {
			if (parent is UnresolvedIdentifier)
				return ((UnresolvedIdentifier)parent).checkMember(context);
			else
				return parent.check(context);
		}
	}

}
