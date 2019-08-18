using prompto.grammar;
using prompto.utils;

namespace prompto.java
{

    public abstract class JavaExpression : IDialectElement
    {
        public abstract void ToDialect(CodeWriter writer);
        public virtual void ParentToDialect(CodeWriter writer)
        {
            ToDialect(writer);
        }
    }
}
