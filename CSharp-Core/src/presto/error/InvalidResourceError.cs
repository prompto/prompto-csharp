using presto.grammar;
using presto.runtime;
using System;
using presto.expression;
using presto.literal;

namespace presto.error
{

    public class InvalidResourceError : ExecutionError
    {

        public InvalidResourceError(String message)
         :   base(message)
        {
        }

        override public IExpression getExpression(Context context)
        {
            return new TextLiteral(base.Message);
        }

    }

}
