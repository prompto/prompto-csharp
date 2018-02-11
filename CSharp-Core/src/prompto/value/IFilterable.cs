using System;
using prompto.runtime;
using prompto.expression;


namespace prompto.value
{
	public interface IFilterable : IValue
	{
		IFilterable Filter (Context context, String itemName, IExpression filter);
	}
}
