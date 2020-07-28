using System;
using prompto.runtime;
using prompto.parser;
using prompto.type;
using prompto.utils;
using prompto.grammar;
using prompto.value;
using prompto.declaration;

namespace prompto.expression
{

    public interface IExpression : IDialectElement {
	    IType check(Context context);
        IType checkReference(Context context);
        IValue interpret(Context context);
        IValue interpretReference(Context context);
        void ParentToDialect(CodeWriter writer);
        AttributeDeclaration CheckAttribute(Context context);
    }

}