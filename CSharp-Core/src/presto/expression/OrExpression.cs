using presto.runtime;
using System;
using presto.error;
using Boolean = presto.value.Boolean;
using presto.parser;
using presto.type;
using presto.utils;
using presto.value;

namespace presto.expression
{

	public class OrExpression : IExpression
	{

		IExpression left;
		IExpression right;

		public OrExpression (IExpression left, IExpression right)
		{
			this.left = left;
			this.right = right;
		}

		public void ToDialect (CodeWriter writer)
		{
			left.ToDialect (writer);
			switch (writer.getDialect ()) {
			case Dialect.E:
			case Dialect.P:
				writer.append (" or ");
				break;
			case Dialect.O:
				writer.append (" || ");
				break;
			}
			right.ToDialect (writer);
		}

		public IType check (Context context)
		{
			IType lt = left.check (context);
			IType rt = right.check (context);
			if (!(lt is BooleanType) || !(rt is BooleanType))
				throw new SyntaxError ("Cannot combine " + lt.getName () + " and " + rt.getName ());
			return BooleanType.Instance;
		}

		public IValue interpret (Context context)
		{
			IValue lval = left.interpret (context);
			IValue rval = right.interpret (context);
			if (lval is Boolean && rval is Boolean)
				return Boolean.ValueOf (((Boolean)lval).Value || ((Boolean)rval).Value);
			else
				throw new SyntaxError ("Illegal: " + lval.GetType ().Name + " + " + rval.GetType ().Name);
		}
	}

}