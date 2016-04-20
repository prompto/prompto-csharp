using System;
using prompto.grammar;
using prompto.runtime;
using prompto.expression;
using prompto.literal;

namespace prompto.error
{

	public class ReadWriteError : ExecutionError
	{

		public ReadWriteError (String message)
			: base (message)
		{
		}

		override
        public IExpression getExpression (Context context)
		{
			return context.getRegisteredValue<CategorySymbol> ("READ_WRITE");
		}

	}
}
