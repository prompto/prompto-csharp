using prompto.runtime;
using System;
using prompto.value;
using prompto.error;
using Decimal = prompto.value.Decimal;
using DateTime = prompto.value.DateTime;
using prompto.parser;
using prompto.type;
using prompto.utils;

namespace prompto.expression
{

    public class SubtractExpression : IExpression
    {

        IExpression left;
        IExpression right;

        public SubtractExpression(IExpression left, IExpression right)
        {
            this.left = left;
            this.right = right;
        }

        public void ToDialect(CodeWriter writer)
        {
			left.ToDialect(writer);
			writer.append(" - ");
			right.ToDialect(writer);
        }

        public IType check(Context context)
        {
            IType lt = left.check(context);
            IType rt = right.check(context);
            return lt.checkSubstract(context, rt);
        }

		public IValue interpret(Context context)
        {
			IValue lval = left.interpret(context);
			IValue rval = right.interpret(context);
            return lval.Subtract(context, rval);
        }
        
    }

}
