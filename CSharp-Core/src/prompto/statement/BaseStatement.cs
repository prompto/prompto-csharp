using prompto.parser;
using System;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.statement
{

    public abstract class BaseStatement : Section, IStatement
    {

        public abstract IType check(runtime.Context context);
        public abstract IValue interpret(runtime.Context context);
        public abstract void ToDialect(CodeWriter writer);
    }

}
