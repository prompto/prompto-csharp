using prompto.memstore;

namespace prompto.store
{
	public abstract class DataStore 
	{
		static IStore instance = new MemStore();

		public static IStore Instance
		{
			get
			{
				return instance;
			}
			set
			{
				instance = value;
			}
		}

	}
}
