using System;
using prompto.type;
using prompto.grammar;

namespace prompto.runtime
{

	/* used for downcast */
	public class LinkedVariable : INamedInstance
	{

		IType type;
		INamed linked;

		public LinkedVariable (IType type, INamed linked)
		{
			this.type = type;
			this.linked = linked;
		}

		public String GetName ()
		{
			return linked.GetName ();
		}

		public IType GetIType (Context context)
		{
			return type;
		}
	}
}