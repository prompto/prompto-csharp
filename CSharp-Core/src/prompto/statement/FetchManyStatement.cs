using prompto.expression;
using prompto.grammar;
using prompto.type;

namespace prompto.statement
{
	public class FetchManyStatement : FetchManyExpression, IStatement
	{
		string name;
		StatementList stmts;

		public FetchManyStatement(CategoryType type, IExpression filter, IExpression first, IExpression last, OrderByClauseList orderBy, string name, StatementList stmts) 
			: base(type, filter, first, last, orderBy)
		{
			this.name = name;
			this.stmts = stmts;
		}

		public bool CanReturn { get { return false; } }

		public bool IsSimple { get { return false; } }

	}
}
