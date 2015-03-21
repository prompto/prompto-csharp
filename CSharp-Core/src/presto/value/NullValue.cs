using presto.type;

namespace presto.value
{
	public class NullValue : BaseValue
	{
		static NullValue instance_ = new NullValue();

		public static NullValue Instance
		{
			get
			{
				return instance_;
			}
		}

		private NullValue() 
			: base(NullType.Instance)
		{
		}
	}
}

