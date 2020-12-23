using System;
using prompto.runtime;
using prompto.type;
using prompto.utils;

namespace prompto.expression
{
    public abstract class PredicateExpression : BaseExpression
    {
        public abstract ArrowExpression ToArrowExpression();
        public abstract IType CheckFilter(Context context, IType itemType);
        public abstract void FilteredToDialect(CodeWriter writer, IExpression source);
        public abstract void ContainsToDialect(CodeWriter writer);
    }
}
