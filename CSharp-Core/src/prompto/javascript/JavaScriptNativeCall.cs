using prompto.grammar;
using prompto.utils;
using prompto.type;
using prompto.runtime;
using System;
using prompto.value;

namespace prompto.javascript
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
