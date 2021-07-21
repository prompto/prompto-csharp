using System;

namespace prompto.store
{
    public interface IAuditMetadata
	{
        object AuditMetadataId { get; set; }
        DateTimeOffset? UTCTimestamp { get; set; }
		string Login { get; set; }
        object this[string key] { get; set; }
    }
}