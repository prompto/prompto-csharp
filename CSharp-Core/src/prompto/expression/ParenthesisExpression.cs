using prompto.error;
using prompto.runtime;
using prompto.store;
using prompto.type;
using prompto.utils;
using prompto.value;


namespace prompto.expression
{

    public class ParenthesisExpression : BaseExpression, IPredicateExpression
    {

        IExpression expression;

        public ParenthesisExpression(IExpression expression)
        {
            this.expression = expression;
        }


		public IExpression getExpression()
		{
			return expression;
		}

		public override string ToString ()
		{
			return "(" + expression.ToString() + ")";
		}

        public override void ToDialect(CodeWriter writer)
        {
			writer.append("(");
			expression.ToDialect(writer);
			writer.append(")");
        }

        public override IType check(Context context)
        {
            return expression.check(context);
        }

		public void checkQuery(Context context)
		{
			if (!(expression is IPredicateExpression))
				throw new SyntaxError("Expected a predicate, found: " + expression.ToString());
			((IPredicateExpression)expression).checkQuery(context);
		}

		public override IValue interpret(Context context)
        {
            return expression.interpret(context);
        }

        public void interpretQuery(Context context, IQueryBuilder builder)
        {
            if (!(expression is IPredicateExpression))
                throw new SyntaxError("Expected a predicate, found: " + expression.ToString());
            ((IPredicateExpression)expression).interpretQuery(context, builder);
        }



    }
}