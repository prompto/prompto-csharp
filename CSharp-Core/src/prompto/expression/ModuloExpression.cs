using System;
using prompto.runtime;
using prompto.error;
using prompto.value;
using prompto.parser;
using prompto.type;
using prompto.utils;


namespace prompto.expression
{

    public class ModuloExpression : BaseExpression, IExpression
    {

        IExpression left;
        IExpression right;

        public ModuloExpression(IExpression left, IExpression right)
        {
            this.left = left;
            this.right = right;
        }

		public override void ToDialect(CodeWriter writer) {
			left.ToDialect(writer);
			writer.append(" % ");
			right.ToDialect(writer);
		}
  
        public override IType check(Context context)
        {
            IType lt = left.check(context);
            IType rt = right.check(context);
            return lt.checkModulo(context, rt);
        }

		public override IValue interpret(Context context)
        {
			IValue lval = left.interpret(context);
			IValue rval = right.interpret(context);
            return lval.Modulo(context, rval);
         }
    }
}
