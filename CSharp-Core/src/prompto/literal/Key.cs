using System;
using prompto.runtime;
using prompto.utils;
using prompto.value;

namespace prompto.literal
{
    public abstract class Key
    {
        internal abstract string interpret(Context context);
        internal abstract void ToDialect(CodeWriter writer);
    }
}
