using System;
using presto.runtime;
using presto.type;
using presto.utils;
using presto.expression;


namespace presto.csharp
{

    public class CSharpThisExpression : CSharpExpression
    {

		ThisExpression expression;

		public CSharpThisExpression()
        {
			this.expression = new ThisExpression();
        }

        public IType check(Context context)
        {
            return expression.check(context);
        }

        public Object interpret(Context context)
        {
            return expression.interpret(context);
        }

		public void ToDialect(CodeWriter writer) {
			expression.ToDialect(writer);
		}

    }

}