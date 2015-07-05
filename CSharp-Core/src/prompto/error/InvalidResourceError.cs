using prompto.grammar;
using prompto.runtime;
using System;
using prompto.expression;
using prompto.literal;

namespace prompto.error
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
