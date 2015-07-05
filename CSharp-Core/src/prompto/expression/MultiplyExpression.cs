using prompto.runtime;
using System;
using prompto.value;
using prompto.error;
using System.Text;
using Decimal = prompto.value.Decimal;
using prompto.parser;
using prompto.type;
using prompto.utils;

namespace prompto.expression
{

    public class MultiplyExpression : IExpression
    {

        IExpression left;
        IExpression right;

        public MultiplyExpression(IExpression left, IExpression right)
        {
            this.left = left;
            this.right = right;
        }

		public void ToDialect(CodeWriter writer) {
			left.ToDialect(writer);
			writer.append(" * ");
			right.ToDialect(writer);
		}

		public IType check(Context context)
        {
            IType lt = left.check(context);
            IType rt = right.check(context);
            return lt.checkMultiply(context, rt, true);
        }

		public IValue interpret(Context context)
        {
			IValue lval = left.interpret(context);
			IValue rval = right.interpret(context);
            return lval.Multiply(context, rval);
        }
  
    }

}
