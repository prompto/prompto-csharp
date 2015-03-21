using presto.runtime;
using System;
using presto.error;
using presto.value;
using Decimal = presto.value.Decimal;
using presto.parser;
using presto.type;
using presto.utils;

namespace presto.expression
{

    public class DivideExpression : IExpression
    {

        IExpression left;
        IExpression right;

        public DivideExpression(IExpression left, IExpression right)
        {
            this.left = left;
            this.right = right;
        }

        public void ToDialect(CodeWriter writer)
        {
			left.ToDialect(writer);
			writer.append(" / ");
			right.ToDialect(writer);
        }

        public IType check(Context context)
        {
            IType lt = left.check(context);
            IType rt = right.check(context);
            return lt.checkDivide(context, rt);
        }

		public IValue interpret(Context context)
        {
			IValue lval = left.interpret(context);
			IValue rval = right.interpret(context);
            return lval.Divide(context, rval);
        }

    }

}