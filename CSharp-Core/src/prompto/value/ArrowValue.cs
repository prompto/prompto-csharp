using System;
using prompto.declaration;
using prompto.expression;
using prompto.runtime;

namespace prompto.value
{
    public class ArrowValue : ContextualExpression
    {
        IMethodDeclaration method;

        public ArrowValue(IMethodDeclaration method, Context calling, ArrowExpression expression)
            : base(calling, expression)
        {
            
            this.method = method;
        }

        public IMethodDeclaration getMethod()
        {
            return method;
        }

        public override IValue interpret(Context context)
        {
            Context parent = context.getParentContext();
		    try {
			    context.setParentContext(Calling);
			    return Expression.interpret(context);
		    } finally {
			    context.setParentContext(parent);
		    }
	    }

    }
}
