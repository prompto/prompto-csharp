using prompto.error;
using prompto.expression;
using prompto.runtime;
using prompto.value;
using prompto.grammar;

namespace prompto.store
{

	/* a mean to store and fetch facts */
	public interface IStore
	{
		void store (Document document);
		Document fetchOne (Context context, IExpression filter) ;
		IDocumentEnumerator fetchMany(Context context, IExpression start, IExpression end, 
			IExpression filter, OrderByClauseList orderBy);
		void flush();
	}

	public abstract class Store
	{
		static IStore instance = new MemStore();

		public static IStore Instance {
			get {
				return instance;
			}
			set {
				instance = value;
			}
		}

	}

}