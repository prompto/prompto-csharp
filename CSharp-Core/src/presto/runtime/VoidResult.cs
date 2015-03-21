using presto.value;
using presto.type;

namespace presto.runtime
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

