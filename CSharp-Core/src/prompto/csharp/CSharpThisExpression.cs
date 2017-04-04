using System;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.expression;


namespace prompto.csharp
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
			writer.append("this");
		}	

    }

}