using System;
using prompto.runtime;
using prompto.type;
using prompto.utils;


namespace prompto.csharp
{

    public class CSharpParenthesisExpression : CSharpExpression
    {

        CSharpExpression expression;

        public CSharpParenthesisExpression(CSharpExpression expression)
        {
            this.expression = expression;
        }

        public override IType check(Context context)
        {
            return expression.check(context);
        }

        public override Object interpret(Context context)
        {
            return expression.interpret(context);
        }

		public override void ToDialect(CodeWriter writer) {
			writer.append('(');
			expression.ToDialect(writer);
			writer.append(')');
		}

    }

}