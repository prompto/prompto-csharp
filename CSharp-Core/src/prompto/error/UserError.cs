using prompto.grammar;
using prompto.runtime;
using prompto.expression;
namespace prompto.error
{

    public class UserError : ExecutionError
    {

        IExpression expression;

        public UserError(IExpression expression)
        {
            this.expression = expression;
        }

        override
        public IExpression getExpression(Context context)
        {
            return expression;
        }
    }

}
