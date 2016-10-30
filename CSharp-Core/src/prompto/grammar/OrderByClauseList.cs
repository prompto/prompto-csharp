using prompto.parser;
using prompto.runtime;
using prompto.store;
using prompto.utils;

namespace prompto.grammar
{

	public class OrderByClauseList : ObjectList<OrderByClause>
	{

		public OrderByClauseList ()
		{	
		}

		public OrderByClauseList (OrderByClause clause)
		{
			this.add (clause);
		}

		public void ToDialect (CodeWriter writer)
		{
			writer.append ("order by ");
			if (writer.getDialect () == Dialect.O)
				writer.append ("( ");
			foreach (OrderByClause clause in this) {
				clause.ToDialect (writer);
				writer.append (", ");
			}
			writer.trimLast (2);
			if (writer.getDialect () == Dialect.O)
				writer.append (" )");
		}

		public void interpretQuery(Context context, IQueryBuilder builder)
		{
			this.ForEach((clause)=>
				clause.interpretQuery(context, builder));
		}


	}
}
