using prompto.error;
using prompto.expression;
using prompto.runtime;
using prompto.value;
using System.Collections.Generic;

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

		private HashSet<Document> instances = new HashSet<Document> ();

		public void store (Document document)
		{
			instances.Add (document);
		}

		public Document fetchOne (Context context, IExpression filter)
		{
			foreach (Document doc in instances) {
				Context local = context.newDocumentContext (doc);
				IValue test = filter.interpret (local);
				if (!(test is Boolean))
					throw new InternalError ("Illegal test result: " + test);
				if (((Boolean)test).Value)
					return doc;
			}
			return null;
		}

	}
}
