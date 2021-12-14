using System;
using prompto.expression;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.value;
using prompto.grammar;
using System.Collections.Generic;

namespace prompto.statement
{
	public class FetchOneStatement : FetchOneExpression, IStatement
	{
		ThenWith thenWith;

		public FetchOneStatement(CategoryType type, IExpression predicate, List<String> include, ThenWith thenWith) 
			: base(type, predicate, include)
		{
			this.thenWith = thenWith;
		}

		public bool CanReturn { get { return false; } }

		public bool IsSimple { get { return false; } }

		public override IType check(runtime.Context context)
		{
			base.check(context);
			return thenWith.check(context, type);
		}

		public override IValue interpret(Context context)
		{
			IValue record = base.interpret(context);
			return thenWith.interpret(context, record);
		}

		public override void ToDialect(CodeWriter writer)
		{
			base.ToDialect(writer);
			thenWith.ToDialect(writer, type);
		}
	}
}
