using System;
using prompto.runtime;

namespace prompto.store
{
	public class QueryInterpreter : QueryInterpreterBase
	{

		public QueryInterpreter(Context context)
			: base(context)
		{
		}

		public override IQuery newQuery()
		{
			return new Query();
		}
	}
}
