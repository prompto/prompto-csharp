using prompto.parser;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.expression
{
    public abstract class BaseExpression : Section, IExpression
    {
        public abstract IType check(Context context);
        public abstract IValue interpret(Context context);
        public abstract void ToDialect(CodeWriter writer);
        public virtual void ParentToDialect(CodeWriter writer)
        {
            ToDialect(writer);
        }
    }
}
