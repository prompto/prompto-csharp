using prompto.grammar;
using System;
using prompto.parser;
using prompto.value;
using prompto.type;
using prompto.expression;
using prompto.runtime;
using prompto.utils;

namespace prompto.value
{

	public class ContextualExpression : BaseValue, IExpression
	{

		Context calling;
		IExpression expression;

		public ContextualExpression (Context calling, IExpression expression)
			: base (null) // TODO check that this is not a problem
		{
			this.calling = calling;
			this.expression = expression;
		}

		override
        public String ToString ()
		{
			return expression.ToString ();
		}

		public void ToDialect (CodeWriter writer)
		{
			expression.ToDialect (writer);
		}

		public IType check (Context context)
		{
			return expression.check (this.calling);
		}

		public IValue interpret (Context context)
		{
			return expression.interpret (calling);
		}

	}

}