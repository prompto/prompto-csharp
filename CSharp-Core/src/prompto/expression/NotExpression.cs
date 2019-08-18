using prompto.runtime;
using System;
using prompto.error;
using Boolean = prompto.value.Boolean;
using prompto.parser;
using prompto.type;
using prompto.utils;
using prompto.value;
using prompto.declaration;
using prompto.store;

namespace prompto.expression
{

	public class NotExpression : BaseExpression, IUnaryExpression, IPredicateExpression, IAssertion
    {

        IExpression expression;

        public NotExpression(IExpression expression)
        {
            this.expression = expression;
        }

        public override IType check(Context context)
        {
            IType type = expression.check(context);
            if (!(type is BooleanType))
				throw new SyntaxError("Cannot negate " + type.GetTypeName());
            return BooleanType.Instance;
        }

        public override void ToDialect(CodeWriter writer)
        {
			writer.append(operatorToDialect(writer.getDialect()));
			expression.ToDialect(writer);
        }
  
		private String operatorToDialect(Dialect dialect) {
			switch(dialect) {
				case Dialect.E:
				case Dialect.M:
					return "not ";
				case Dialect.O:
					return "!";
				default:
					throw new Exception("Unsupported: " + dialect);	
			}
		}

		public override IValue interpret(Context context)
		{
			IValue val = expression.interpret (context);
			return interpret (val);
		}

		public IValue interpret(IValue val)
		{
            if (val is Boolean)
                return ((Boolean)val).Not;
            else
                throw new SyntaxError("Illegal: not " + val.GetType().Name);
        }

		public bool interpretAssert(Context context, TestMethodDeclaration test) {
			IValue val = expression.interpret(context);
			IValue result = interpret(val);
			if(result==Boolean.TRUE) 
				return true;
			CodeWriter writer = new CodeWriter(test.Dialect, context);
			this.ToDialect(writer);
			String expected = writer.ToString();
			String actual = operatorToDialect(test.Dialect) + val.ToString();
			test.printAssertionFailed(context, expected, actual);
			return false;
		}

		public void interpretQuery(Context context, IQueryBuilder builder)
		{
			if (!(expression is IPredicateExpression))
				throw new SyntaxError("Not a predicate: " + expression.ToString());
			((IPredicateExpression)expression).interpretQuery(context, builder);
			builder.Not();
		}

    }

}
