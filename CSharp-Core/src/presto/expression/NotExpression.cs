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

	public class NotExpression : IUnaryExpression, IAssertion
    {

        IExpression expression;

        public NotExpression(IExpression expression)
        {
            this.expression = expression;
        }

        public IType check(Context context)
        {
            IType type = expression.check(context);
            if (!(type is BooleanType))
				throw new SyntaxError("Cannot negate " + type.GetName());
            return BooleanType.Instance;
        }

        public void ToDialect(CodeWriter writer)
        {
			writer.append(operatorToDialect(writer.getDialect()));
			expression.ToDialect(writer);
        }
  
		private String operatorToDialect(Dialect dialect) {
			switch(dialect) {
				case Dialect.E:
				case Dialect.S:
					return "not ";
				case Dialect.O:
					return "!";
				default:
					throw new Exception("Unsupported: " + dialect);	
			}
		}

		public IValue interpret(Context context)
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
			test.printFailure(context, expected, actual);
			return false;
		}

    }

}
