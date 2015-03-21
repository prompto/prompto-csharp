using System;
using presto.value;
using presto.type;

namespace presto.runtime
{


	/* used to ensure downcast local resolves to actual value */
	public class LinkedValue : BaseValue
	{

		Context context;

		public LinkedValue (Context context, IType type)
			: base(type)
		{
			this.context = context;
		}

		public Context getContext ()
		{
			return context;
		}

	}
}