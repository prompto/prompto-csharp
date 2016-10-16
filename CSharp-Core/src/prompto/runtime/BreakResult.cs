using prompto.value;
using prompto.type;

namespace prompto.runtime
{
	public class BreakResult : BaseValue
	{
		static BreakResult instance = new BreakResult();

		private BreakResult()
			: base(VoidType.Instance)
		{
		}

		public static BreakResult Instance {
			get 
			{
				return instance;
			}
		}
	}
}

