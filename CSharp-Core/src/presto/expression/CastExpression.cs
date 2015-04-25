using System;
using presto.type;
using presto.runtime;
using presto.error;
using presto.utils;
using presto.parser;
using presto.value;

namespace presto.expression {

	public class CastExpression : IExpression {

		IExpression expression;
		IType type;

		public CastExpression(IExpression expression, IType type) {
			this.expression = expression;
			this.type = type;
		}

		public IType check(Context context) {
			IType actual = expression.check(context);
			if(!type.isAssignableTo(context, actual))
				throw new SyntaxError("Cannot cast " + actual.ToString() + " to " + type.ToString());
			return type;
		}

		public IValue interpret(Context context) {
			return expression.interpret(context);
		}

		public void ToDialect(CodeWriter writer) {
			switch(writer.getDialect()) {
			case Dialect.E:
			case Dialect.S:
				expression.ToDialect(writer);
				writer.append(" as ");
				type.ToDialect(writer);
				break;
			case Dialect.O:
				writer.append("(");
				type.ToDialect(writer);
				writer.append(")");
				expression.ToDialect(writer);
				break;
			}

		}

	}

}