using System;
using prompto.type;
using prompto.runtime;
using prompto.error;
using prompto.utils;
using prompto.parser;
using prompto.value;

namespace prompto.expression {

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
			IValue value = expression.interpret(context);
			if (value != null)
			{
				if (type == DecimalType.Instance && value is Integer)
					value = new value.Decimal(((Integer)value).DecimalValue);
				else if (type == IntegerType.Instance && value is value.Decimal)
					value = new Integer(((value.Decimal)value).IntegerValue);
				else if (type.isMoreSpecificThan(context, value.GetIType()))
					value.SetIType(type);
			}
			return value;
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