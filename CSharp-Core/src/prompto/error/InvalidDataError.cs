using System;
using prompto.grammar;
using prompto.runtime;
using prompto.expression;
using prompto.literal;
namespace prompto.error
{

    public class InvalidDataError : ExecutionError
    {

        public InvalidDataError(String message)
            : base(message)
        {
        }

        override
        public IExpression getExpression(Context context)
        {
            return new TextLiteral(base.Message);
        }

    }
}
