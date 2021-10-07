using System.Collections.Generic;
using System;
using System.Threading;
using System.Collections;
using System.Linq;
using prompto.store;
using System.Reflection;

namespace prompto.memstore
{

    /* a utility class for running unit tests only */
    public class MemStore : IStore
    {

        long lastDbId = 0;
        Dictionary<string, long> sequences = new Dictionary<string, long>();
        Dictionary<object, AuditMetadata> auditMetadatas = new Dictionary<object, AuditMetadata>();
        long lastAuditMetadataId = 0;
        Dictionary<object, AuditRecord> auditRecords = new Dictionary<object, AuditRecord>();
        long lastAuditRecordDbId = 0;

        public long NextDbId
        {
            get
            {
                return Interlocked.Increment(ref lastDbId);
            }
        }


        public long NextSequenceValue(string name)
        {
            lock (sequences)
            {
                if (sequences.ContainsKey(name))
                {
                    long value = sequences[name] + 1;
                    sequences[name] = value;
                    return value;
                }
                else
                {
                    sequences[name] = 1;
                    return 1;
                }
            }
        }


        private Dictionary<long, StorableDocument> documents = new Dictionary<long, StorableDocument>();

        public void DeleteAndStore(ICollection<object> idsToDelete, ICollection<IStorable> docsToStore, IAuditMetadata auditMeta)
        {
            auditMeta = StoreAuditMetadata(auditMeta);
            if (idsToDelete != null)
                DoDelete(idsToDelete, auditMeta);
            if (docsToStore != null)
                DoStore(docsToStore, auditMeta); ;
        }

        private void DoStore(ICollection<IStorable> docsToStore, IAuditMetadata auditMeta)
        {
            foreach (IStorable storable in docsToStore)
            {
                DoStore(storable, auditMeta);
            }
        }

        private void DoStore(IStorable storable, IAuditMetadata auditMeta)
        {
            AuditOperation operation = AuditOperation.UPDATE;
            // ensure db id
            StorableDocument doc = (StorableDocument)storable;
            long dbId = doc.DbId is long ? (long)doc.DbId : -1;
            if(dbId == -1)
            {
                dbId = ++lastDbId;
                storable.SetData("dbId", dbId);
            }
            if (!documents.ContainsKey(dbId))
                 operation = AuditOperation.INSERT;
            documents[dbId] = (StorableDocument)storable;
            AuditRecord audit = NewAuditRecord(auditMeta);
            audit.InstanceDbId = dbId;
            audit.Operation = operation;
            audit.Instance = doc;
            auditRecords[audit.DbId] = audit;
        }

        private void DoDelete(ICollection<object> idsToDelete, IAuditMetadata auditMeta)
        {
            foreach (object id in idsToDelete)
            {
                DoDelete(id, auditMeta);
            }
        }

        private void DoDelete(object dbId, IAuditMetadata auditMeta)
        {
            documents.Remove((long)dbId);
            AuditRecord audit = NewAuditRecord(auditMeta);
            audit.InstanceDbId = dbId;
            audit.Operation = AuditOperation.DELETE;
            auditRecords[audit.DbId] = audit;
        }

        public Boolean IsAuditEnabled()
        {
            return true;
        }

        private AuditRecord NewAuditRecord(IAuditMetadata auditMeta)
        {
            AuditRecord audit = new AuditRecord();
            audit.DbId = ++lastAuditRecordDbId;
            audit.MetadataDbId = auditMeta.DbId;
            audit.UTCTimestamp = auditMeta.UTCTimestamp;
            return audit;
        }

        private IAuditMetadata StoreAuditMetadata(IAuditMetadata auditMeta)
        {
            if (auditMeta == null)
                auditMeta = NewAuditMetadata();
            auditMetadatas[auditMeta.DbId] = (AuditMetadata)auditMeta;
            return auditMeta;
        }

        public IAuditMetadata NewAuditMetadata()
        {
            AuditMetadata meta = new AuditMetadata();
            meta.DbId = ++lastAuditMetadataId;
            meta.UTCTimestamp = DateTimeOffset.UtcNow;
            return meta;
        }

        public void Flush()
        {
            // nothing to do
        }

        public IStored FetchUnique(object dbId)
        {
            StorableDocument stored;
            if (documents.TryGetValue((long)dbId, out stored))
                return stored;
            else
                return null;
        }


        public IQueryBuilder NewQueryBuilder()
        {
            return new QueryBuilder();
        }


        public IStored FetchOne(IQuery query)
        {
            IPredicate predicate = ((Query)query).GetPredicate();
            foreach (StorableDocument doc in documents.Values)
            {
                if (doc.matches(predicate))
                    return doc;
            }
            return null;
        }

        public IStoredEnumerable FetchMany(IQuery query)
        {
            Query q = (Query)query;
            List<StorableDocument> allDocs = FetchManyDocs(q);
            long totalCount = allDocs.Count;
            List<StorableDocument> slicedDocs = Slice(q, allDocs);
            return new StorableDocumentEnumerable(slicedDocs, totalCount);
        }


        private List<StorableDocument> FetchManyDocs(Query query)
        {
            List<StorableDocument> docs = FilterDocs(query.GetPredicate());
            docs = Sort(query.GetOrdering(), docs);
            return docs;
        }

        private List<StorableDocument> FilterDocs(IPredicate predicate)
        {
            // create list of filtered docs
            List<StorableDocument> docs = new List<StorableDocument>();
            foreach (StorableDocument doc in documents.Values)
            {
                if (doc.matches(predicate))
                    docs.Add(doc);
            }
            return docs;
        }

        private List<StorableDocument> Slice(Query query, List<StorableDocument> docs)
        {
            if (docs == null || docs.Count == 0)
                return docs;
            long? first = query.GetFirst();
            long? last = query.GetLast();
            if (first == null && last == null)
                return docs;
            if (first == null || first < 1)
                first = 1L;
            if (last == null || last > docs.Count)
                last = docs.Count;
            if (first > last)
                return new List<StorableDocument>();
            return docs.Skip((int)(first - 1)).Take((int)(1 + last - first)).ToList();
        }

        private List<StorableDocument> Sort(ICollection<OrderBy> orderBy, List<StorableDocument> docs)
        {
            if (orderBy == null || orderBy.Count == 0 || docs.Count < 2)
                return docs;
            List<bool> directions = new List<bool>();
            foreach (OrderBy o in orderBy)
                directions.Add(o.isDescending());
            docs.Sort((o1, o2) =>
            {
                DataTuple v1 = ReadTuple(o1, orderBy);
                DataTuple v2 = ReadTuple(o2, orderBy);
                return v1.CompareTo(v2, directions);
            });
            return docs;
        }


        private DataTuple ReadTuple(StorableDocument doc, ICollection<OrderBy> orderBy)
        {
            DataTuple tuple = new DataTuple();
            foreach (OrderBy o in orderBy)
                tuple.Add(doc.GetData(o.getAttributeInfo().getName()));
            return tuple;
        }

        public IStorable NewStorable(List<string> categories, IDbIdFactory factory)
        {
            return new StorableDocument(categories, new DbIdFactoryProxy(this, factory));
        }

        class DbIdFactoryProxy : IDbIdFactory
        {
            private MemStore memStore;
            private IDbIdFactory factory;

            public DbIdFactoryProxy(MemStore memStore, IDbIdFactory factory)
            {
                this.memStore = memStore;
                this.factory = factory;
            }

            public IDbIdProvider Provider {
                get
                {
                    return () =>
                    {
                        object dbId = factory != null ? factory.Provider.Invoke() : null;
                        return dbId != null ? dbId : memStore.NextDbId;
                    };
                }
            }

            public IDbIdListener Listener
            {
                get
                {
                    return dbId =>
                    {
                        if (factory != null)
                            factory.Listener.Invoke(dbId);
                    };
                }
            }

            public bool IsUpdate()
            {
                return true;
            }
        }


        public Type GetDbIdType()
        {
            return typeof(long);
        }

        class DataTuple : List<object>
        {
            public int CompareTo(DataTuple other, List<bool> directions)
            {
                IEnumerator<bool> iterDirs = directions.GetEnumerator();
                IEnumerator<object> iterThis = GetEnumerator();
                IEnumerator<object> iterOther = other.GetEnumerator();
                while (iterThis.MoveNext())
                {
                    bool descending = iterDirs.MoveNext() ? iterDirs.Current : false;
                    if (iterOther.MoveNext())
                    {
                        // compare items
                        object thisVal = iterThis.Current;
                        object otherVal = iterOther.Current;
                        if (thisVal == null && otherVal == null)
                            continue;
                        else if (thisVal == null)
                            return descending ? 1 : -1;
                        else if (otherVal == null)
                            return descending ? -1 : 1;
                        else if (thisVal is IComparable)
                        {
                            int cmp = ((IComparable)thisVal).CompareTo(otherVal);
                            // if not equal, done
                            if (cmp != 0)
                                return descending ? -cmp : cmp;
                        }
                        else
                            return 0; // TODO throw ?
                    }
                    else
                        return descending ? -1 : 1;
                }
                bool desc2 = iterDirs.MoveNext() ? iterDirs.Current : false;
                if (iterOther.MoveNext())
                    return desc2 ? 1 : -1;
                else
                    return 0;
            }
        }

        class StorableDocumentEnumerable : IStoredEnumerable
        {
            List<StorableDocument> slicedDocs;
            long totalCount;

            public StorableDocumentEnumerable(List<StorableDocument> slicedDocs, long totalCount)
            {
                this.slicedDocs = slicedDocs;
                this.totalCount = totalCount;
            }

            public long Length
            {
                get { return slicedDocs.Count; }
            }
            public long TotalLength
            {
                get
                {
                    return totalCount;
                }
            }

            public IEnumerator<IStored> GetEnumerator()
            {
                return new StorableDocumentEnumerator(slicedDocs, totalCount);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new StorableDocumentEnumerator(slicedDocs, totalCount);
            }

            IStoredEnumerator IStoredEnumerable.GetEnumerator()
            {
                return new StorableDocumentEnumerator(slicedDocs, totalCount);
            }
        }

        class StorableDocumentEnumerator : IStoredEnumerator
        {
            List<StorableDocument> docs;
            readonly long totalCount;
            IEnumerator<StorableDocument> iter;

            public StorableDocumentEnumerator(List<StorableDocument> docs, long totalCount)
            {
                this.docs = docs;
                this.totalCount = totalCount;
                iter = docs.GetEnumerator();
            }

            public bool MoveNext()
            {
                return iter.MoveNext();
            }

            object IEnumerator.Current
            {
                get
                {
                    return iter.Current;
                }
            }

            public IStored Current
            {
                get
                {
                    return iter.Current;
                }
            }

            public long Length
            {
                get
                {
                    return docs.Count;
                }
            }

            public long TotalLength
            {
                get
                {
                    return totalCount;
                }
            }

            public void Dispose()
            {
            }

            public void Reset()
            {
                throw new Exception("Unsupported!");
            }
        }


        class StorableDocument : IStored, IStorable
        {

            Dictionary<string, object> document = null;
            readonly List<string> categories;
            IDbIdFactory dbIdFactory;

            public StorableDocument(List<string> categories, IDbIdFactory dbIdFactory)
            {
                this.categories = categories;
                this.dbIdFactory = dbIdFactory;
            }

            public List<string> Categories
            {
                get
                {
                    return categories;
                }
            }

            public ISet<String> Names
            {
                get
                {
                    return document == null ? new HashSet<String>() : new HashSet<String>(document.Keys);
                }
            }

            public object GetOrCreateDbId()
            {
                object dbId = GetData("dbId");
                if (dbId == null)
                {
                    dbId = dbIdFactory.Provider.Invoke();
                    SetData("dbId", dbId);
                    dbIdFactory.Listener.Invoke(dbId);
                }
                return dbId;
            }

            public object DbId
            {
                get
                {
                    return GetData("dbId");
                }
            }

            public bool Dirty
            {
                get
                {
                    return document != null;
                }
                set
                {
                    if (!value)
                        document = null;
                    else if (document == null)
                        document = newDocument(null);
                }
            }



            public void SetData(string name, object value)
            {
                if (document == null)
                    document = newDocument(null);
                document[name] = value;
            }

            private Dictionary<string, object> newDocument(object dbId)
            {
                Dictionary<string, object> doc = new Dictionary<string, object>();
                if (categories != null)
                    doc["category"] = categories;
                doc["dbId"] = dbId != null ? dbId : dbIdFactory.Provider.Invoke();
                return doc;
            }

            public bool matches(IPredicate predicate)
            {
                if (predicate == null)
                    return true;
                return predicate.matches(document);
            }

            public object GetData(string name)
            {
                object value;
                if (document!=null && document.TryGetValue(name, out value))
                    return value;
                else
                    return null;
            }
        }

        public object FetchLatestAuditMetadataId(object dbId)
        {
            return FetchAuditMetadataIdsStream(dbId).First();
        }

        public List<object> FetchAllAuditMetadataIds(object dbId)
        {
            return FetchAuditMetadataIdsStream(dbId).ToList();
        }

        IEnumerable<object> FetchAuditMetadataIdsStream(object dbId)
        {
            return auditRecords.Values
                .Where(a => dbId.Equals(a.InstanceDbId))
                .OrderByDescending(a => a.UTCTimestamp)
                .Select(a => a.MetadataDbId);
        }

        public IAuditMetadata FetchAuditMetadata(object dbId)
        {
            return auditMetadatas[dbId];
        }

        public IDictionary<string, object> FetchAuditMetadataAsDocument(object dbId)
        {
            return auditMetadatas[dbId];
        }

        public IAuditRecord FetchLatestAuditRecord(object dbId)
        {
            return auditRecords.Values
                .Where(a => dbId.Equals(a.InstanceDbId))
                .OrderByDescending(a => a.UTCTimestamp)
                .First();
        }

        public IDictionary<string, object> FetchLatestAuditRecordAsDocument(object dbId)
        {
            IAuditRecord record = FetchLatestAuditRecord(dbId);
            return record == null ? null : ((AuditRecord)record).AsDocument();
        }

        public List<IAuditRecord> FetchAllAuditRecords(object dbId)
        {
            return FetchAllAuditRecordsStream(dbId)
                .ToList<IAuditRecord>();
        }

        public List<IDictionary<string, object>> FetchAllAuditRecordsAsDocuments(object dbId)
        {
            return FetchAllAuditRecordsStream(dbId)
                 .Select(a => a.AsDocument())
                 .ToList();
        }

        IEnumerable<AuditRecord> FetchAllAuditRecordsStream(object dbId)
        {
            return auditRecords.Values
                .Where(a => dbId.Equals(a.InstanceDbId))
                .OrderByDescending(a => a.UTCTimestamp);
        }


        public List<object> FetchDbIdsAffectedByAuditMetadataId(object dbId)
        {
            return auditRecords.Values
                .Where(a => dbId.Equals(a.MetadataDbId))
                .OrderByDescending(a => a.UTCTimestamp)
                .Select(a => a.DbId)
                .ToList();
       }

        public List<IAuditRecord> FetchAuditRecordsMatching(IDictionary<string, object> auditPredicates, IDictionary<string, object> instancePredicates)
        {
            return FetchAuditRecordsMatchingStream(auditPredicates, instancePredicates)
                    .ToList<IAuditRecord>();
        }

        public List<IDictionary<string, object>> FetchAuditRecordsMatchingAsDocuments(IDictionary<string, object> auditPredicates, IDictionary<string, object> instancePredicates)
        {
            return FetchAuditRecordsMatchingStream(auditPredicates, instancePredicates)
                     .Select(a => a.AsDocument())
                    .ToList();
        }

        IEnumerable<AuditRecord> FetchAuditRecordsMatchingStream(IDictionary<string, object> auditPredicates, IDictionary<string, object> instancePredicates)
        {
            return auditRecords.Values
                .Where(a => a.Matches(auditPredicates, instancePredicates))
                .OrderByDescending(a => a.UTCTimestamp);
        }

        class AuditRecord : IAuditRecord
        {
            public object DbId { get; set; }
            public object MetadataDbId { get; set; }
            public DateTimeOffset? UTCTimestamp { get; set; }
            public object InstanceDbId { get; set; }
            public AuditOperation Operation { get; set; }
            public IStored Instance { get; set; }

            public override String ToString()
            {
                return AsDocument().ToString();
            }

            public IDictionary<string, object> AsDocument()
            {
                IDictionary<string, object> doc = new Dictionary<string, object>();
                doc["dbId"] = DbId;
                doc["metadataDbId"] = MetadataDbId;
                doc["utcTimeStamp"] = UTCTimestamp;
                doc["instanceDbId"] = InstanceDbId;
                doc["operation"] = Operation.ToString();
                if (Instance != null)
                    doc["instance"] = ConvertInstance(Instance);
                return doc;
            }

            private object ConvertInstance(IStored instance)
            {
                IDictionary<string, object> doc = new Dictionary<string, object>();
                foreach (string name in instance.Names)
                    doc[name] = ConvertValue(instance.GetData(name));
                return doc;
            }

            private object ConvertValue(object value)
            {
                if (value == null)
                    return null;
                else
                    return value; // TODO convert to Prompto native types if required
            }

            internal bool Matches(IDictionary<string, object> auditPredicates, IDictionary<string, object> instancePredicates)
            {
                // at least 1 predicate is mandatory
                if ((auditPredicates == null ? 0 : auditPredicates.Count) + (instancePredicates == null ? 0 : instancePredicates.Count) == 0)
                    return false;
                else
                    return (auditPredicates == null
                            || auditPredicates.All(kvp => AuditMatches(kvp)))
                        && (instancePredicates == null
                            || instancePredicates.All(kvp => InstanceMatches(kvp)));
            }

            internal bool AuditMatches(KeyValuePair<string, object> predicate)
            {
                Matcher matcher = FindOrCreateMatcher(predicate.Key);
                return matcher != null && matcher.Invoke(this, predicate.Value);
            }

     
            internal bool InstanceMatches(KeyValuePair<string, object> predicate)
            {
                return Instance != null && Object.Equals(Instance.GetData(predicate.Key), predicate.Value);
            }

            internal delegate bool Matcher(AuditRecord audit, Object value);

            static readonly IDictionary<string, Matcher> MATCHERS = new Dictionary<string, Matcher>();

            internal Matcher FindOrCreateMatcher(String name)
            {
                Matcher matcher = null;
                if(!MATCHERS.TryGetValue(name, out matcher))
                {
                    FieldInfo field = typeof(AuditRecord).GetField(name);
                    if(field.FieldType.IsEnum)
                        matcher = (record, value) => Object.Equals(field.GetValue(record), value);
                    else
                        matcher = (record, value) => Object.Equals(field.GetValue(record), value);
                }
                return matcher;
            }

        }


        class AuditMetadata : Dictionary<string, Object>, IAuditMetadata
        {
            public object DbId
            {
                get
                {
                    return this["dbId"];
                }
                set {
                    this["dbId"] = value;
                }
            }

            public DateTimeOffset? UTCTimestamp {
                get {
                    return (DateTimeOffset?)this["utcTimeStamp"];
                }
                set {
                    this["utcTimeStamp"] = value;
                }
            }

            public string Login {
                get {
                    return (string)this["login"];
                }

                set {
                    this["login"] = value;
                }
            }
        }
    }

}
