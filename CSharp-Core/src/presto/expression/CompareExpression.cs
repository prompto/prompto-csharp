using presto.runtime;
using System;
using presto.error;
using presto.value;
using Decimal = presto.value.Decimal;
using Boolean = presto.value.Boolean;
using DateTime = presto.value.DateTime;
using presto.parser;
using presto.type;
using presto.grammar;
using presto.utils;

namespace presto.expression
{

    public class CompareExpression : IExpression
    {

        IExpression left;
        CmpOp oper;
        IExpression right;

        public CompareExpression(IExpression left, CmpOp oper, IExpression right)
        {
            this.left = left;
            this.oper = oper;
            this.right = right;
        }

        public void ToDialect(CodeWriter writer)
        {
			left.ToDialect(writer);
			writer.append(" ");
			OperToDialect(writer);
			writer.append(" ");
			right.ToDialect(writer);
        }

		public void OperToDialect(CodeWriter writer) {
			switch(oper) {
			case CmpOp.GT:
				writer.append(">");
				break;
			case CmpOp.GTE:
				writer.append(">=");
				break;
			case CmpOp.LT:
				writer.append("<");
				break;
			case CmpOp.LTE:
				writer.append("<=");
				break;
			}
		}


        public IType check(Context context)
        {
            IType lt = left.check(context);
            IType rt = right.check(context);
            return lt.checkCompare(context, rt);
        }

        public IValue interpret(Context context)
        {
			IValue lval = left.interpret(context);
			IValue rval = right.interpret(context);
            return compare(context, lval, rval);
        }

        private Boolean compare(Context context, IValue lval, IValue rval)
        {
            Int32 cmp = lval.CompareTo(context, rval);
            switch (oper)
            {
                case CmpOp.GT:
                    return Boolean.ValueOf(cmp > 0);
                case CmpOp.LT:
                    return Boolean.ValueOf(cmp < 0);
                case CmpOp.GTE:
                    return Boolean.ValueOf(cmp >= 0);
                case CmpOp.LTE:
                    return Boolean.ValueOf(cmp <= 0);
                default:
                    throw new SyntaxError("Illegal compare operand: " + oper.ToString());
            }
        }
    }
}
