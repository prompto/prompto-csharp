using prompto.runtime;
using prompto.error;
using System;
using Boolean = prompto.value.Boolean;
using prompto.parser;
using prompto.type;
using prompto.utils;
using prompto.value;
using prompto.declaration;
using prompto.store;

namespace prompto.expression
{

	public class AndExpression : IPredicateExpression, IAssertion
    {

        IExpression left;
        IExpression right;

        public AndExpression(IExpression left, IExpression right)
        {
            this.left = left;
            this.right = right;
        }

		public IExpression Left
		{
			get
			{
				return this.left;
			}
		}

		public IExpression Right
		{
			get
			{
				return this.right;
			}
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
			case Dialect.M:
				return " and ";
			case Dialect.O:
				return " && ";
			default:
				throw new Exception ("Unsupported!");
			}
		}

        public IType check(Context context)
        {
            IType lt = left.check(context);
            IType rt = right.check(context);
            if (!(lt is BooleanType) || !(rt is BooleanType))
				throw new SyntaxError("Cannot combine " + lt.GetTypeName() + " and " + rt.GetTypeName());
            return BooleanType.Instance;
        }

		public IValue interpret(Context context)
		{
			IValue lval = left.interpret (context);
			if (!(lval is Boolean))
				throw new SyntaxError("Illegal: " + lval.GetType().Name + " and ..., expected a Boolean");
			if (!((Boolean)lval).Value)
				return lval;
			IValue rval = right.interpret (context);
			if (!(rval is Boolean))
	            throw new SyntaxError("Illegal: " + lval.GetType().Name + " and " + rval.GetType().Name);
    		return rval;
		}

		public bool interpretAssert(Context context, TestMethodDeclaration test) {
			IValue lval = left.interpret(context);
			if (!(lval is Boolean))
				throw new SyntaxError("Illegal: " + lval.GetType().Name + " and ..., expected a Boolean");
			IValue rval = lval;
			if (((Boolean)lval).Value)
			{
				rval = right.interpret(context);
				if (!(rval is Boolean))
					throw new SyntaxError("Illegal: " + lval.GetType().Name + " and " + rval.GetType().Name);
			}
			if(rval==Boolean.TRUE) 
				return true;
			CodeWriter writer = new CodeWriter(test.Dialect, context);
			this.ToDialect(writer);
			String expected = writer.ToString();
			String actual = lval + operatorToDialect(test.Dialect) + rval;
			test.printAssertionFailed(context, expected, actual);
			return false;
		}

		public void interpretQuery(Context context, IQueryBuilder builder)
		{
			if (!(left is IPredicateExpression))
				throw new SyntaxError("Not a predicate: " + left);
			((IPredicateExpression)left).interpretQuery(context, builder);
			if (!(right is IPredicateExpression))
				throw new SyntaxError("Not a predicate: " + right);
			((IPredicateExpression)right).interpretQuery(context, builder);
			builder.And();
		}

    }
}
