using prompto.runtime;
using prompto.parser;
using System;
using prompto.grammar;
using prompto.type;
using prompto.utils;

namespace prompto.declaration
{

    public interface IDeclaration : INamed, ISection {
	    void register(Context context);
	    IType check(Context context);
        void ToDialect(CodeWriter writer);
    }
}