using prompto.error;
using prompto.expression;
using prompto.runtime;
using prompto.value;
using System.Collections.Generic;
using prompto.grammar;
using System;
using System.Collections;
using System.Linq;

namespace prompto.store
{

	/* a utility class for running unit tests only */
	public class MemStore : IStore
	{

		static MemStore instance = new MemStore ();

		public static MemStore Instance
		{
			get {
				return instance;
			}
		}

		private HashSet<Document> documents = new HashSet<Document> ();

		public void store (Document document)
		{
			documents.Add (document);
		}

		public Document fetchOne (Context context, IExpression filter)
		{
			foreach (Document doc in documents) {
				if (matches (context, doc, filter))
					return doc;
			}
			return null;
		}

		private bool matches(Context context, Document doc, IExpression filter) {
			if(filter==null)
				return true;
			Context local = context.newDocumentContext (doc);
			IValue test = filter.interpret (local);
			if (!(test is prompto.value.Boolean))
				throw new InternalError ("Illegal test result: " + test);
			return ((prompto.value.Boolean)test).Value;
		}


		class DocumentEnumerator : IDocumentEnumerator
		{
			List<Document> docs;
			IEnumerator<Document> iter;

			public DocumentEnumerator(List<Document> docs)
			{
				this.docs = docs;
				this.iter = docs.GetEnumerator();
			}

			public bool MoveNext()
			{ 
				return iter.MoveNext(); 
			}

			object IEnumerator.Current  { 
				get { 
					return iter.Current; 
				} 
			}

			public Document Current { 
				get { 
					return iter.Current; 
				} 
			}

			public long Length { 
				get { 
					return docs.Count; 
				} 
			}

			public void Dispose()
			{
			}

			public void Reset()
			{
				throw new Exception ("Unsupported!");
			}
		}

		public IDocumentEnumerator fetchMany(Context context, IExpression start, IExpression end, 
			IExpression filter, OrderByClauseList orderBy) {
			List<Document> docs = fetchManyDocs(context, start, end, filter, orderBy);
			return new DocumentEnumerator(docs);
		}


		private List<Document> fetchManyDocs(Context context, IExpression start, IExpression end, 
			IExpression filter, OrderByClauseList orderBy) {
			List<Document> docs = filterDocs(context, filter);
			// sort it if required
			docs = sort(context, docs, orderBy);
			// slice it if required
			docs = slice(context, docs, start, end);
			// done
			return docs;
		}

		private List<Document> filterDocs(Context context, IExpression filter) {
			// create list of filtered docs
			List<Document> docs = new List<Document>();
			foreach(Document doc in documents) {
				if(matches(context, doc, filter))
					docs.Add(doc);
			}
			return docs;
		}

		private List<Document> slice(Context context, List<Document> docs, IExpression start, IExpression end) {
			if(docs.Count==0)
				return docs;
			if(start==null && end==null)
				return docs;
			Int64? startValue = null;
			Int64? endValue = null;
			if(start!=null) {
				IValue value = start.interpret(context);
				if(value==null)
					throw new NullReferenceError();
				else if(!(value is Integer))
					throw new SyntaxError("Expecting an integer, got " + value.GetType(context).GetName());
				startValue = ((Integer)value).IntegerValue;
			}
			if(end!=null) {
				IValue value = end.interpret(context);
				if(value==null)
					throw new NullReferenceError();
				else if(!(value is Integer))
					throw new SyntaxError("Expecting an integer, got " + value.GetType(context).GetName());
				endValue = ((Integer)value).IntegerValue;
			}
			if(startValue==null || startValue<1)
				startValue = 1L;
			if(endValue==null || endValue>docs.Count)
				endValue = docs.Count;
			if(startValue>docs.Count || startValue > endValue)
				return new List<Document>();
			return docs.Skip((Int32)startValue - 1).Take(1 + (Int32)(endValue - startValue)).ToList();
		}

		private List<Document> sort(Context context, List<Document> docs, OrderByClauseList orderBy) {
			if(orderBy!=null) {
				List<bool> directions = collectDirections(orderBy);
				docs.Sort( (o1, o2) => {
						TupleValue v1 = readValue(context, o1, orderBy);
						TupleValue v2 = readValue(context, o2, orderBy);
						return v1.CompareTo(context, v2, directions);
					});
			}
			return docs;
		}

		private List<bool> collectDirections(OrderByClauseList orderBy) {
			List<bool> list = new List<bool>();
			foreach(OrderByClause clause in orderBy)
				list.Add(clause.isDescending());
			return list;
		}

		private TupleValue readValue(Context context, Document doc, OrderByClauseList orderBy) {
			TupleValue tuple = new TupleValue();
			foreach(OrderByClause clause in orderBy)
				tuple.add(readValue(context, doc, clause));
			return tuple;
		}

		private IValue readValue(Context context, Document doc, OrderByClause clause) {
			IValue source = doc;
			IValue value = null;
			foreach(String name in clause.getNames()) {
				if(!(source is Document))
					return null;
				value = source.GetMember(context, name);
				source = value;
			}
			return value;
		}

	}
}
