using presto.parser;
using System;
using presto.type;
using presto.utils;
using presto.value;

namespace presto.statement
{

    public abstract class BaseStatement : Section, IStatement
    {

        public abstract IType check(runtime.Context context);
        public abstract IValue interpret(runtime.Context context);
        public abstract void ToDialect(CodeWriter writer);
    }

}
