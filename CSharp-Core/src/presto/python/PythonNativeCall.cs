using presto.grammar;
using presto.parser;
using presto.runtime;
using presto.type;
using System;
using presto.utils;
using presto.value;

namespace presto.python
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
