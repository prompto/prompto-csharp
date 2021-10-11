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
		Boolean IsAuditEnabled();
		IAuditMetadata NewAuditMetadata();
		object FetchLatestAuditMetadataId(object dbId);
		List<object> FetchAllAuditMetadataIds(object dbId);
		IAuditMetadata FetchAuditMetadata(object dbId);
		IDictionary<string, object> FetchAuditMetadataAsDocument(object dbId);
		bool DeleteAuditMetadata(object dbId);
		IAuditRecord FetchLatestAuditRecord(object dbId);
		IDictionary<string, object> FetchLatestAuditRecordAsDocument(object dbId);
		List<IAuditRecord>	FetchAllAuditRecords(object dbId);
		List<IDictionary<string, object>> FetchAllAuditRecordsAsDocuments(object dbId);
		List<object> FetchDbIdsAffectedByAuditMetadataId(object dbId);
		bool DeleteAuditRecord(object dbId);
		List<IAuditRecord> FetchAuditRecordsMatching(IDictionary<string, object> auditPredicates, IDictionary<string, object> instancePredicates);
		List<IDictionary<string, object>> FetchAuditRecordsMatchingAsDocuments(IDictionary<string, object> auditPredicates, IDictionary<string, object> instancePredicates);

	}

}