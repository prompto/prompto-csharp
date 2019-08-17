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

        public IExpression Expression { get; set; }
        public Context Calling { get; set; }

        public ContextualExpression (Context calling, IExpression expression)
			: base (null) // TODO check that this is not a problem
		{
			this.Calling = calling;
			this.Expression = expression;
		}

		
        public override String ToString ()
		{
			return Expression.ToString ();
		}

		public void ToDialect (CodeWriter writer)
		{
            Expression.ToDialect (writer);
		}


        public IType check (Context context)
		{
			return Expression.check (Calling);
		}

		public virtual IValue interpret (Context context)
		{
			return Expression.interpret (Calling);
		}

	}

}