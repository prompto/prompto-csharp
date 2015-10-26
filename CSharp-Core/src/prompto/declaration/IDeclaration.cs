using prompto.runtime;
using prompto.parser;
using System;
using prompto.grammar;
using prompto.type;
using prompto.utils;
using System.Collections.Generic;
using prompto.statement;

namespace prompto.declaration
{

    public interface IDeclaration : INamed, ISection {
	    void register(Context context);
	    IType check(Context context);
        void ToDialect(CodeWriter writer);
		List<CommentStatement> Comments { get; set; }
    }
}