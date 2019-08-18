using System;
using prompto.parser;
using prompto.utils;


namespace prompto.grammar
{

    public abstract class NativeCategoryBinding : IDialectElement
    {
        public abstract void ToDialect (CodeWriter writer);
        public virtual void ParentToDialect(CodeWriter writer)
        {
            ToDialect(writer);
        }
   }

}