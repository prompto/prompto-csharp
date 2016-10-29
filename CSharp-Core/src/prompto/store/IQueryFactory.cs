using System;

namespace prompto.store
{
	public interface IQueryFactory
	{
		IQuery newQuery();
	}
}
