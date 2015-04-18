using presto.runtime;
using System;
using presto.error;
using Boolean = presto.value.Boolean;
using presto.expression;
using presto.type;
using presto.utils;
using presto.value;

namespace presto.grammar
{

	public class MatchingExpressionConstraint : IAttributeConstraint
	{

		IExpression expression;

		public MatchingExpressionConstraint (IExpression expression)
		{
			this.expression = expression;
		}

		public void checkValue (Context context, IValue value)
		{
			Context child = context.newChildContext ();
			child.registerValue (new Variable ("value", AnyType.Instance));
			child.setValue ("value", value);
			IValue test = expression.interpret (child);
			if (test != Boolean.TRUE)
				throw new InvalidDataError ((value == null ? "null" : value.ToString ()) + " does not match:" + expression.ToString ());
		}

		public void ToDialect (CodeWriter writer)
		{
			writer.append (" matching ");
			expression.ToDialect (writer);
		}


	}
}