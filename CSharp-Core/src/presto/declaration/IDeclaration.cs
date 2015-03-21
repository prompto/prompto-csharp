using presto.runtime;
using presto.parser;
using System;
using presto.grammar;
using presto.type;
using presto.utils;

namespace presto.declaration
{

    public interface IDeclaration : INamed, ISection {
	    void register(Context context);
	    IType check(Context context);
        void ToDialect(CodeWriter writer);
    }
}