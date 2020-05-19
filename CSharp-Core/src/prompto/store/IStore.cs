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
		void Store (ICollection<object> idsToDelete, ICollection<IStorable> docsToStore);
		IQueryBuilder NewQueryBuilder();
		IStored FetchUnique(object dbId);
		IStored FetchOne(IQuery query);
		IStoredEnumerable FetchMany(IQuery query);
		void Flush();
		IStorable NewStorable(List<string> categories);
		long NextSequenceValue(string name);
	}

}