using prompto.grammar;
using prompto.parser;
using prompto.runtime;
using prompto.type;
using System;
using prompto.utils;
using prompto.value;

namespace prompto.python
{

    public class PythonNativeCall : NativeCall
    {

        PythonStatement statement;

        public PythonNativeCall(PythonStatement statement)
        {
            this.statement = statement;
        }

		override
		public void ToDialect(CodeWriter writer)
        {
			statement.ToDialect(writer);
        }

        public override IType check(Context context)
        {
            return VoidType.Instance; // TODO
        }

        public override IValue interpret(Context context)
        {
            throw new Exception("Should never get there!");
        }

    }
}
