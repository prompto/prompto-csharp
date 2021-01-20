using prompto.expression;
using prompto.grammar;
using prompto.parser;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.statement
{
	public class FetchManyStatement : FetchManyExpression, IStatement
	{
		ThenWith thenWith;

		public FetchManyStatement(CategoryType type, IExpression filter, IExpression first, IExpression last, OrderByClauseList orderBy, ThenWith thenWith)
			: base(type, filter, first, last, orderBy)
		{
			this.thenWith = thenWith;
		}

		public bool CanReturn { get { return false; } }

		public bool IsSimple { get { return false; } }


		public override IType check(Context context)
		{
			base.check(context);
			return thenWith.check(context, new CursorType(type));
		}


		public override IValue interpret(Context context) 
		{
			IValue record = base.interpret(context);
			return thenWith.interpret(context, record);
		}

		public override void ToDialect(CodeWriter writer)
		{
			base.ToDialect(writer);
			thenWith.ToDialect(writer, new CursorType(type));
		}


	}
}
