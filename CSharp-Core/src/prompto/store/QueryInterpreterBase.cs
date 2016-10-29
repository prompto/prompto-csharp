using System;
using prompto.declaration;
using prompto.expression;
using prompto.grammar;
using prompto.runtime;
using prompto.store;
using prompto.type;
using prompto.value;

namespace prompto.store
{
	public abstract class QueryInterpreterBase : IQueryInterpreter
	{

		protected Context context;

		protected QueryInterpreterBase(Context context)
		{
			this.context = context;
		}

		public IQuery buildFetchOneQuery(CategoryType type, IPredicateExpression predicate)
		{
			IQuery q = newQuery();
			if (type != null)
				q.verify<String>(new AttributeInfo("category", TypeFamily.TEXT, true, null), MatchOp.CONTAINS, type.GetTypeName());
			if (predicate != null)
				predicate.interpretQuery(context, q);
			if (type != null && predicate != null)
				q.and();
			return q;
		}

		public IQuery buildFetchManyQuery(CategoryType type,
						IExpression start, IExpression end,
						IPredicateExpression predicate,
                          OrderByClauseList orderBy)
		{
			IQuery q = newQuery();
			if (type != null)
				q.verify<String>(new AttributeInfo("category", TypeFamily.TEXT, true, null), MatchOp.CONTAINS, type.GetTypeName());
			q.setFirst(getLimit(context, start));
			q.setLast(getLimit(context, end));
			if (orderBy != null)
				orderBy.interpretQuery(context, q);
			if (predicate != null)
				predicate.interpretQuery(context, q);
			if (type != null && predicate != null)
				q.and();
			return q;
		}

		private Int64? getLimit(Context context, IExpression exp)
		{
			if (exp == null)
				return null;
			IValue value = exp.interpret(context);
			if (!(value is Integer))
				throw new InvalidValueError("Expecting an Integer, got:" + value.GetIType().ToString());
			return ((Integer)value).IntegerValue;
		}

		public abstract IQuery newQuery();
	}
}