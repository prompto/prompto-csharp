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

    public class NotExpression : IUnaryExpression
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
                throw new SyntaxError("Cannot negate " + type.getName());
            return BooleanType.Instance;
        }

        public void ToDialect(CodeWriter writer)
        {
			switch(writer.getDialect()) {
			case Dialect.E:
			case Dialect.P:
				writer.append("not ");
				break;
			case Dialect.O:
				writer.append("!");
				break;
			}
			expression.ToDialect(writer);
        }
  

		public IValue interpret(Context context)
        {
			IValue val = expression.interpret(context);
            if (val is Boolean)
                return ((Boolean)val).Not;
            else
                throw new SyntaxError("Illegal: not " + val.GetType().Name);
        }
    }

}
