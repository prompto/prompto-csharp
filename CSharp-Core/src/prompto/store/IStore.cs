using prompto.error;
using prompto.expression;
using prompto.runtime;
using prompto.value;

namespace prompto.store
{

	/* a mean to store and fetch facts */
	public interface IStore
	{
		void store (Document document);
		Document fetchOne (Context context, IExpression filter) ;
	}

	public abstract class Store
	{
		static IStore instance = null;

		public static IStore Instance {
			get {
				return instance;
			}
		}

	}

}