using System;

namespace prompto.store
{
    public interface IAuditRecord
    {
		object DbId { get; set; }
		object MetadataDbId { get; set; }
		DateTimeOffset? UTCTimestamp { get; set; }
		object InstanceDbId { get; set; }
		AuditOperation Operation { get; set; }
		IStored Instance { get; set; }
    }
}
