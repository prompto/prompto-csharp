using prompto.runtime;
using System;
using prompto.error;
using BooleanValue = prompto.value.BooleanValue;
using prompto.parser;
using prompto.type;
using prompto.utils;
using prompto.value;
using prompto.declaration;
using prompto.store;

namespace prompto.expression
{

	public class OrExpression : BaseExpression, IPredicateExpression, IAssertion
	{

		IExpression left;
		IExpression right;

		public OrExpression (IExpression left, IExpression right)
		{
			this.left = left;
			this.right = right;
		}

		public override void ToDialect(CodeWriter writer)
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
			case Dialect.M:
				return " or ";
			case Dialect.O:
				return " || ";
			default:
				throw new Exception ("Unsupported!");
			}
		}

		public override IType check (Context context)
		{
			IType lt = left.check (context);
			IType rt = right.check (context);
			if (!(lt is BooleanType) || !(rt is BooleanType))
				throw new SyntaxError ("Cannot combine " + lt.GetTypeName () + " and " + rt.GetTypeName ());
			return BooleanType.Instance;
		}

		public void checkQuery(Context context)
		{
			if (!(left is IPredicateExpression))
				throw new SyntaxError("Expected a predicate, got: " + left.ToString());
			((IPredicateExpression)left).checkQuery(context);
			if (!(right is IPredicateExpression))
				throw new SyntaxError("Expected a predicate, got: " + right.ToString());
			((IPredicateExpression)right).checkQuery(context);
		}

		public override IValue interpret (Context context)
		{
			IValue lval = left.interpret (context);
			IValue rval = right.interpret (context);
			return interpret (lval, rval);
		}

		public IValue interpret(IValue lval, IValue rval)
		{
			if (lval is BooleanValue && rval is BooleanValue)
				return BooleanValue.ValueOf (((BooleanValue)lval).Value || ((BooleanValue)rval).Value);
			else
				throw new SyntaxError ("Illegal: " + lval.GetType ().Name + " + " + rval.GetType ().Name);
		}


		public bool interpretAssert(Context context, TestMethodDeclaration test) {
			IValue lval = left.interpret(context);
			IValue rval = right.interpret(context);
			IValue result = interpret(lval, rval);
			if(result==BooleanValue.TRUE) 
				return true;
			CodeWriter writer = new CodeWriter(test.Dialect, context);
			this.ToDialect(writer);
			String expected = writer.ToString();
			String actual = lval.ToString() + operatorToDialect(test.Dialect) + rval.ToString();
			test.printAssertionFailed(context, expected, actual);
			return false;
		}

		public void interpretQuery(Context context, IQueryBuilder builder)
		{
			if (!(left is IPredicateExpression))
				throw new SyntaxError("Not a predicate: " + left.ToString());
			((IPredicateExpression)left).interpretQuery(context, builder);
			if (!(right is IPredicateExpression))
				throw new SyntaxError("Not a predicate: " + right.ToString());
			((IPredicateExpression)right).interpretQuery(context, builder);
			builder.Or();
		}

	}

}