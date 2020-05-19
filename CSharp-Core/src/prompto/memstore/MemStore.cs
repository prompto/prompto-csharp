using System.Collections.Generic;
using System;
using System.Threading;
using System.Collections;
using System.Linq;
using prompto.store;

namespace prompto.memstore
{

	/* a utility class for running unit tests only */
	public class MemStore : IStore
	{

		static long lastDbId = 0;
        static Dictionary<string, long> sequences = new Dictionary<string, long>();

		public static long NextDbId
		{
			get
			{
				return Interlocked.Increment(ref lastDbId);
			}
		}


		public long NextSequenceValue(string name)
		{
            lock(sequences)
            {
                if(sequences.ContainsKey(name))
                {
					long value = sequences[name] + 1;
					sequences[name] = value;
					return value;
				} else
                {
					sequences[name] = 1;
					return 1;
				}
			}
		}


		private Dictionary<long, StorableDocument> documents = new Dictionary<long, StorableDocument>();

		public void Store(ICollection<object> idsToDelete, ICollection<IStorable> docsToStore)
		{
			if (idsToDelete != null)
			{
				foreach (object id in idsToDelete)
				{
					documents.Remove((long)id);
				}
			}
			if (docsToStore != null)
			{
				foreach (IStorable storable in docsToStore)
				{
					object dbId = storable.GetOrCreateDbId();
					documents[(long)dbId] = (StorableDocument)storable;
				}
			}
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

		public IStorable NewStorable(List<string> categories)
		{
			return new StorableDocument(categories);
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

			public StorableDocument(List<string> categories)
			{
				this.categories = categories;
			}

			public object GetOrCreateDbId()
			{
				object dbId = GetData("dbId");
				if (dbId == null)
				{
					dbId = NextDbId;
					SetData("dId", dbId);
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
				doc["dbId"] = dbId == null ? NextDbId : dbId;
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
				if (document.TryGetValue(name, out value))
					return value;
				else
					return null;
			}
		}



	}

}
