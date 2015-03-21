using presto.grammar;
using presto.runtime;
using presto.expression;
namespace presto.error
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
