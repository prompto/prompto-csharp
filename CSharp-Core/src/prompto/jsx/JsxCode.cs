using System;
using prompto.declaration;
using prompto.error;
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
        string suite;

		public JsxCode(IExpression expression, String suite)
		{
			this.expression = expression;
            this.suite = suite;
		}


		public IType check(Context context)
		{
			expression.check(context);
			return JsxType.Instance;
		}

		public virtual IType checkReference(Context context)
		{
			return check(context);
		}

		public AttributeDeclaration CheckAttribute(Context context)
		{
			throw new SyntaxError("Expected an attribute, got: " + this.ToString());
		}

		public IValue interpret(Context context)
		{
			return new JsxValue(this);
		}

		public virtual IValue interpretReference(Context context)
		{
			return interpret(context);
		}

		public void ToDialect(CodeWriter writer)
		{
			writer.append("{");
			expression.ToDialect(writer);
			writer.append("}");
            if (suite != null)
                writer.appendRaw(suite);
		}

        public void ParentToDialect(CodeWriter writer)
        {
            ToDialect(writer);
        }


    }
}