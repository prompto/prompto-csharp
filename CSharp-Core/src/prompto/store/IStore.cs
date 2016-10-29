using prompto.error;
using prompto.expression;
using prompto.runtime;
using prompto.value;
using prompto.grammar;
using System;
using System.Collections.Generic;
using prompto.type;

namespace prompto.store
{

	/* a mean to store and fetch facts */
	public interface IStore
	{
		Type GetDbIdType(); 
		void store (ICollection<object> idsToDelete, ICollection<IStorable> docsToStore);
		IQueryInterpreter getQueryInterpreter(Context context);
		IQueryFactory getQueryFactory();
		IStored interpretFetchOne (Context context, CategoryType category, IPredicateExpression filter);
		IStoredEnumerable interpretFetchMany(Context context, CategoryType category, 
		                                       IExpression start, IExpression end, 
												IPredicateExpression filter, OrderByClauseList orderBy);
		IStored fetchUnique(object dbId);
		IStored fetchOne(IQuery query);
		IStoredEnumerable fetchMany(IQuery query);
		void flush();
		IStorable NewStorable(List<string> categories);
	}

}