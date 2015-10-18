using prompto.error;
using prompto.runtime;
using System;
using prompto.parser;
using prompto.type;
using prompto.value;
using prompto.utils;

namespace prompto.expression
{

    public class DocumentExpression : IExpression
    {

        public IType check(Context context)
        {
            return DocumentType.Instance;
        }

		public IValue interpret(Context context)
        {
            return new Document();
        }

        public void ToDialect(CodeWriter writer)
        {
			writer.append("Document");
			if(writer.getDialect()!=Dialect.E)
				writer.append("()");
        }
    }

}