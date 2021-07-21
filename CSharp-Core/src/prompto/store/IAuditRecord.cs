using System;

namespace prompto.store
{
    public interface IAuditRecord
    {
		object AuditRecordId { get; set; }
		object AuditMetadataId { get; set; }
		DateTimeOffset? UTCTimestamp { get; set; }
		object InstanceDbId { get; set; }
		AuditOperation Operation { get; set; }
		IStored Instance { get; set; }
	}
}
