using System;
using prompto.runtime;
using prompto.expression;


namespace prompto.value
{
	public interface IFilterable : IValue
	{
		IFilterable Filter (Predicate<IValue> filter);
	}
}
