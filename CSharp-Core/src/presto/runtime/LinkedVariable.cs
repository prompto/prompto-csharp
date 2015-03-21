using System;
using presto.type;
using presto.grammar;

namespace presto.runtime
{

	/* used for downcast */
	public class LinkedVariable : INamed
	{

		IType type;
		INamed linked;

		public LinkedVariable (IType type, INamed linked)
		{
			this.type = type;
			this.linked = linked;
		}

		public String getName ()
		{
			return linked.getName ();
		}

		public IType GetType (Context context)
		{
			return type;
		}
	}
}