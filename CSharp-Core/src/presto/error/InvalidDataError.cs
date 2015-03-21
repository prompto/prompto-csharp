using System;
using presto.grammar;
using presto.runtime;
using presto.expression;
using presto.literal;
namespace presto.error
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
