using presto.grammar;
using presto.utils;
using presto.type;
using presto.runtime;
using System;
using presto.value;

namespace presto.javascript
{

	public class JavaScriptNativeCall : NativeCall
	{

		JavaScriptStatement statement;

		public JavaScriptNativeCall (JavaScriptStatement statement)
		{
			this.statement = statement;
		}

		override
		public void ToDialect (CodeWriter writer)
		{
			writer.append ("JavaScript: ");
			statement.ToDialect (writer);
		}

		override
		public IType check (Context context)
		{
			return VoidType.Instance; // TODO
		}

		override
		public IValue interpret (Context context)
		{
			throw new Exception ("Should never get there!");
		}
	}
}
