using presto.value;
using presto.type;
using presto.runtime;
using presto.utils;

namespace presto.expression
{

	public class DefaultExpression : IExpression
	{

		IExpression expression;
		IValue value;

		public DefaultExpression (IExpression expression)
		{
			this.expression = expression;
		}

		public IType check (Context context)
		{
			return expression.check (context);
		}

		public IValue interpret (Context context)
		{
			if (value == null)
				value = expression.interpret (context);
			return value;
		}

		public void ToDialect (CodeWriter writer)
		{
			expression.ToDialect (writer);
		}
	}
}
