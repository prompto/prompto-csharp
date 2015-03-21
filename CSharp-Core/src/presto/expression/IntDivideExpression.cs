using System;
using presto.runtime;
using presto.error;
using presto.value;
using presto.parser;
using presto.type;
using presto.utils;


namespace presto.expression
{

    public class IntDivideExpression : IExpression
    {

        IExpression left;
        IExpression right;

        public IntDivideExpression(IExpression left, IExpression right)
        {
            this.left = left;
            this.right = right;
        }

        public void ToDialect(CodeWriter writer)
        {
			left.ToDialect (writer);
			writer.append (" \\ ");
			right.ToDialect(writer);
        }
 
        public IType check(Context context)
        {
            IType lt = left.check(context);
            IType rt = right.check(context);
            return lt.checkIntDivide(context, rt);
        }

        public IValue interpret(Context context)
        {
			IValue lval = left.interpret(context);
			IValue rval = right.interpret(context);
            return lval.IntDivide(context, rval);
        }
 
    }

}
