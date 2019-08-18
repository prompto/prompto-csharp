using prompto.runtime;
using prompto.value;
using prompto.utils;
using prompto.type;

namespace prompto.expression
{

	public class PlusExpression : BaseExpression
    {

        IExpression left;
        IExpression right;

        public PlusExpression(IExpression left, IExpression right)
        {
            this.left = left;
            this.right = right;
        }

		public override string ToString ()
		{
			return left.ToString() + " + " + right.ToString();
		}

        public override void ToDialect(CodeWriter writer)
        {
			left.ToDialect(writer);
			writer.append(" + ");
			right.ToDialect(writer);
        }

        public override IType check(Context context)
        {
            IType lt = left.check(context);
            IType rt = right.check(context);
            return lt.checkAdd(context, rt, true);
        }

        public override IValue interpret(Context context)
        {
			IValue lval = left.interpret(context);
			IValue rval = right.interpret(context);
            return lval.Add(context, rval);
        }

    }

}
