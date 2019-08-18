using prompto.value;
using prompto.type;
using prompto.runtime;
using prompto.utils;

namespace prompto.expression
{

	public class DefaultExpression : BaseExpression, IExpression
	{

		IExpression expression;
		IValue value;

		public DefaultExpression (IExpression expression)
		{
			this.expression = expression;
		}

		public override IType check (Context context)
		{
			return expression.check (context);
		}

		public override IValue interpret (Context context)
		{
			if (value == null)
				value = expression.interpret (context);
			return value;
		}

		public override void ToDialect (CodeWriter writer)
		{
			expression.ToDialect (writer);
		}
	}
}
