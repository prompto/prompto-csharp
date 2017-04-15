using prompto.runtime;
using System;
using prompto.parser;
using prompto.type;
using prompto.expression;
using prompto.utils;
using prompto.value;

namespace prompto.statement
{

	public class ReturnStatement : SimpleStatement
    {

        IExpression expression;

        public ReturnStatement(IExpression expression)
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
			writer.append("return");
			if (expression != null) {
				writer.append (" ");
				expression.ToDialect (writer);
			}
        }

        override
        public bool Equals(Object obj)
        {
            if (obj == this)
                return true;
            if (obj == null)
                return false;
            if (!(obj is ReturnStatement))
                return false;
			IExpression other = ((ReturnStatement)obj).getExpression();
			if (expression == other)
				return true;
			else if (expression == null || other == null)
				return false;
			else
				return expression.Equals(other);
        }

        override
        public IType check(Context context)
        {
			return expression==null ? VoidType.Instance : expression.check(context);
        }

        override
		public IValue interpret(Context context)
        {
			if (expression == null)
				return VoidResult.Instance;
			else {
				IValue value = expression.interpret (context);
				return value == null ? NullValue.Instance : value;
			}
      }

		public override bool CanReturn
		{
			get
			{
				return true;
			}
		}

    }

}