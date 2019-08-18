using prompto.expression;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.jsx
{
	public class JsxCode : IJsxExpression
	{

		IExpression expression;

		public JsxCode(IExpression expression)
		{
			this.expression = expression;
		}


		public IType check(Context context)
		{
			expression.check(context);
			return JsxType.Instance;
		}

		public IValue interpret(Context context)
		{
			return new JsxValue(this);
		}

        public void ToDialect(CodeWriter writer)
		{
			writer.append("{");
			expression.ToDialect(writer);
			writer.append("}");
		}

        public void ParentToDialect(CodeWriter writer)
        {
            ToDialect(writer);
        }


    }
}