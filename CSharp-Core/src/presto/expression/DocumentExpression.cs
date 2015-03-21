using presto.error;
using presto.runtime;
using System;
using presto.parser;
using presto.type;
using presto.value;
using presto.utils;

namespace presto.expression
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
			if(writer.getDialect()==Dialect.O)
				writer.append("()");
        }
    }

}