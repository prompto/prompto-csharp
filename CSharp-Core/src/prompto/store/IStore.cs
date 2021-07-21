using System;
using System.Collections.Generic;

namespace prompto.store
{

	/* a mean to store and fetch facts */
	public interface IStore
	{
		Type GetDbIdType(); 
		void DeleteAndStore (ICollection<object> idsToDelete, ICollection<IStorable> docsToStore, IAuditMetadata auditMeta);
		IQueryBuilder NewQueryBuilder();
		IStored FetchUnique(object dbId);
		IStored FetchOne(IQuery query);
		IStoredEnumerable FetchMany(IQuery query);
		void Flush();
		IStorable NewStorable(List<string> categories, IDbIdFactory factory);
		long NextSequenceValue(string name);
		IAuditMetadata NewAuditMetadata();
		object FetchLatestAuditMetadataId(object dbId);
		List<object> FetchAllAuditMetadataIds(object dbId);
		IAuditMetadata FetchAuditMetadata(object dbId);
		IDictionary<string, object> FetchAuditMetadataAsDocument(object dbId);

	}

}