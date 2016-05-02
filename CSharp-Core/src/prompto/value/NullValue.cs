using prompto.type;

namespace prompto.value
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

		public override object ConvertTo (System.Type type)
		{
			return null;
		}
	}
}

