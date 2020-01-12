using prompto.runtime;
using System;
using prompto.error;
using BooleanValue = prompto.value.BooleanValue;
using prompto.expression;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.constraint
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
			if (test != BooleanValue.TRUE)
				throw new InvalidDataError ((value == null ? "null" : value.ToString ()) + " does not match:" + expression.ToString ());
		}

		public void ToDialect (CodeWriter writer)
		{
			writer.append (" matching ");
			expression.ToDialect (writer);
		}


	}
}
