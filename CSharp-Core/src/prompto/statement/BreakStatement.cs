using prompto.runtime;
using System;
using prompto.parser;
using prompto.type;
using prompto.expression;
using prompto.utils;
using prompto.value;

namespace prompto.statement
{

	public class BreakStatement : SimpleStatement
	{

		override
		public void ToDialect(CodeWriter writer)
		{
			writer.append("break");
		}

		override
		public bool Equals(Object obj)
		{
			return (obj is BreakStatement);
		}

		override
		public IType check(Context context)
		{
			return VoidType.Instance;
		}

		override
		public IValue interpret(Context context)
		{
			return BreakResult.Instance;
		}

		public override bool CanReturn
		{
			get
			{
				return true;
			}
		}

	}

}