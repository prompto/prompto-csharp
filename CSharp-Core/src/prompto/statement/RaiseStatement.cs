using System;
using prompto.runtime;
using prompto.error;
using prompto.parser;
using prompto.type;
using prompto.expression;
using prompto.utils;
using prompto.value;


namespace prompto.statement
{

	public class RaiseStatement : SimpleStatement
    {

        IExpression expression;

        public RaiseStatement(IExpression expression)
        {
            this.expression = expression;
        }

        public IExpression getExpression()
        {
            return expression;
        }

        override
        public void ToDialect(CodeWriter writer)
        {
			switch(writer.getDialect()) {
			case Dialect.E:
			case Dialect.S:
				writer.append("raise ");
				break;
			case Dialect.O:
				writer.append("throw ");
				break;
			}
			expression.ToDialect(writer);
        }

        override
        public bool Equals(Object obj)
        {
            if (obj == this)
                return true;
            if (obj == null)
                return false;
            if (!(obj is RaiseStatement))
                return false;
            RaiseStatement other = (RaiseStatement)obj;
            return this.getExpression().Equals(other.getExpression());
        }

        override
        public IType check(Context context)
        {
            IType type = expression.check(context);
            if (!type.isAssignableTo(context, new CategoryType("Error")))
				throw new SyntaxError(type.GetTypeName() + " does not extend Error");
			return VoidType.Instance;
        }

        override
        public IValue interpret(Context context)
        {
            throw new UserError(expression);
        }

    }

}
