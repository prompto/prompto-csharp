using prompto.runtime;
using prompto.value;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Collections;
using System.Linq;
using prompto.type;
using prompto.expression;
using prompto.grammar;

namespace prompto.store
{

	/* a utility class for running unit tests only */
	public class MemStore : IStore
	{

		static long lastDbId = 0;

		public static long NextDbId
		{
			get
			{
				return Interlocked.Increment(ref lastDbId);
			}
		}

		private Dictionary<long, StorableDocument> documents = new Dictionary<long, StorableDocument>();

		public void store(ICollection<object> idsToDelete, ICollection<IStorable> docsToStore)
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

		public void flush()
		{
			// nothing to do
		}

		public IStored fetchUnique(object dbId)
		{
			StorableDocument stored;
			if (documents.TryGetValue((long)dbId, out stored))
				return stored;
			else
				return null;
		}


		public IStored interpretFetchOne(Context context, CategoryType category, IPredicateExpression filter)
		{
			return fetchOne(getQueryInterpreter(context).buildFetchOneQuery(category, filter));
		}

		public IStoredEnumerable interpretFetchMany(Context context, CategoryType category,
						IExpression start, IExpression end,
						IPredicateExpression filter,
						OrderByClauseList orderBy)
		{
			return fetchMany(getQueryInterpreter(context)
				.buildFetchManyQuery(category, start, end, filter, orderBy));
		}

		public IStored fetchOne(IQuery query)
		{
			IPredicate predicate = ((Query)query).GetPredicate();
			foreach (StorableDocument doc in documents.Values)
			{
				if (doc.matches(predicate))
					return doc;
			}
			return null;
		}

		public IQueryInterpreter getQueryInterpreter(Context context)
		{
			return new QueryInterpreter(context);
		}

		public IQueryFactory getQueryFactory()
		{
			throw new NotImplementedException();
		}

		public IStoredEnumerable fetchMany(IQuery query)
		{
			List<StorableDocument> allDocs = fetchManyDocs(query);
			List<StorableDocument> slicedDocs = slice(query, allDocs);
			return new StorableDocumentEnumerable(allDocs, slicedDocs);
		}


		private List<StorableDocument> fetchManyDocs(IQuery query)
		{
			List<StorableDocument> docs = filterDocs(((Query)query).GetPredicate());
			docs = sort(((Query)query).getOrdering(), docs);
			return docs;
		}

		private List<StorableDocument> filterDocs(IPredicate predicate)
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

		private List<StorableDocument> slice(IQuery query, List<StorableDocument> docs)
		{
			if (docs == null || docs.Count == 0)
				return docs;
			long? first = query.getFirst();
			long? last = query.getLast();
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

		private List<StorableDocument> sort(ICollection<IOrderBy> orderBy, List<StorableDocument> docs)
		{
			if (orderBy == null || orderBy.Count == 0 || docs.Count < 2)
				return docs;
			List<bool> directions = new List<bool>();
			foreach (IOrderBy o in orderBy)
				directions.Add(o.isDescending());
			docs.Sort((o1, o2) =>
			{
				DataTuple v1 = readTuple(o1, orderBy);
				DataTuple v2 = readTuple(o2, orderBy);
				return v1.CompareTo(v2, directions);
			});
			return docs;
		}


		private DataTuple readTuple(StorableDocument doc, ICollection<IOrderBy> orderBy)
		{
			DataTuple tuple = new DataTuple();
			foreach (IOrderBy o in orderBy)
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
			List<StorableDocument> allDocs;
			List<StorableDocument> slicedDocs;

			public StorableDocumentEnumerable(List<StorableDocument> allDocs, List<StorableDocument> slicedDocs)
			{
				this.allDocs = allDocs;
				this.slicedDocs = slicedDocs;
			}

			public long Length
			{
				get { return slicedDocs.Count; }
			}
			public long TotalLength
			{
				get
				{
					return allDocs.Count;
				}
			}

			public IEnumerator<IStored> GetEnumerator()
			{
				return new StorableDocumentEnumerator(slicedDocs);
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return new StorableDocumentEnumerator(slicedDocs);
			}

			IStoredEnumerator IStoredEnumerable.GetEnumerator()
			{
				return new StorableDocumentEnumerator(slicedDocs);
			}
		}

		class StorableDocumentEnumerator : IStoredEnumerator
		{
			List<StorableDocument> docs;
			IEnumerator<StorableDocument> iter;

			public StorableDocumentEnumerator(List<StorableDocument> docs)
			{
				this.docs = docs;
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
