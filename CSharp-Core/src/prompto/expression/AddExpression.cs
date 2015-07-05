using System;
using prompto.runtime;
using prompto.value;
using prompto.error;
using System.Globalization;
using prompto.utils;
using Decimal = prompto.value.Decimal;
using DateTime = prompto.value.DateTime;
using prompto.parser;
using prompto.type;

namespace prompto.expression
{

	public class AddExpression : IExpression
    {

        IExpression left;
        IExpression right;

        public AddExpression(IExpression left, IExpression right)
        {
            this.left = left;
            this.right = right;
        }

		public override string ToString ()
		{
			return left.ToString() + " + " + right.ToString();
		}

        public void ToDialect(CodeWriter writer)
        {
			left.ToDialect(writer);
			writer.append(" + ");
			right.ToDialect(writer);
        }

        public IType check(Context context)
        {
            IType lt = left.check(context);
            IType rt = right.check(context);
            return lt.checkAdd(context, rt, true);
        }

        public IValue interpret(Context context)
        {
			IValue lval = left.interpret(context);
			IValue rval = right.interpret(context);
            return lval.Add(context, rval);
        }

    }

}
