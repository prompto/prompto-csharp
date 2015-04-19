using presto.runtime;
using System;
using presto.error;
using Boolean = presto.value.Boolean;
using presto.parser;
using presto.type;
using presto.utils;
using presto.value;
using presto.declaration;

namespace presto.expression
{

	public class OrExpression : IExpression, IAssertion
	{

		IExpression left;
		IExpression right;

		public OrExpression (IExpression left, IExpression right)
		{
			this.left = left;
			this.right = right;
		}

		public void ToDialect(CodeWriter writer)
		{
			left.ToDialect(writer);
			writer.append(operatorToDialect(writer.getDialect()));
			right.ToDialect(writer);
		}

		private String operatorToDialect(Dialect dialect) 
		{
			switch(dialect) 
			{
			case Dialect.E:
			case Dialect.P:
				return " or ";
			case Dialect.O:
				return " || ";
			default:
				throw new Exception ("Unsupported!");
			}
		}

		public IType check (Context context)
		{
			IType lt = left.check (context);
			IType rt = right.check (context);
			if (!(lt is BooleanType) || !(rt is BooleanType))
				throw new SyntaxError ("Cannot combine " + lt.GetName () + " and " + rt.GetName ());
			return BooleanType.Instance;
		}

		public IValue interpret (Context context)
		{
			IValue lval = left.interpret (context);
			IValue rval = right.interpret (context);
			return interpret (lval, rval);
		}

		public IValue interpret(IValue lval, IValue rval)
		{
			if (lval is Boolean && rval is Boolean)
				return Boolean.ValueOf (((Boolean)lval).Value || ((Boolean)rval).Value);
			else
				throw new SyntaxError ("Illegal: " + lval.GetType ().Name + " + " + rval.GetType ().Name);
		}


		public bool interpretAssert(Context context, TestMethodDeclaration test) {
			IValue lval = left.interpret(context);
			IValue rval = right.interpret(context);
			IValue result = interpret(lval, rval);
			if(result==Boolean.TRUE) 
				return true;
			CodeWriter writer = new CodeWriter(test.Dialect, context);
			this.ToDialect(writer);
			String expected = writer.ToString();
			String actual = lval.ToString() + operatorToDialect(test.Dialect) + rval.ToString();
			test.printFailure(context, expected, actual);
			return false;
		}
	}

}