using prompto.value;
using prompto.type;

namespace prompto.runtime
{
	public class VoidResult : BaseValue
	{
		static VoidResult instance = new VoidResult();

		private VoidResult()
			: base(VoidType.Instance)
		{
		}

		public static VoidResult Instance {
			get 
			{
				return instance;
			}
		}
	}
}

