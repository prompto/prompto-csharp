using System;
using prompto.declaration;
using prompto.error;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.jsx
{

	public class JsxText : IJsxExpression
	{

		String text;


		public JsxText(String text)
		{
			this.text = text;
		}

		public IType check(Context context)
		{
			return TextType.Instance;
		}

		public virtual IType checkReference(Context context)
		{
			return check(context);
		}

		public AttributeDeclaration CheckAttribute(Context context)
		{
			throw new SyntaxError("Expected an attribute, found: " + this.ToString());
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
			writer.append(text);
		}

        public void ParentToDialect(CodeWriter writer)
        {
            ToDialect(writer);
        }


    }
}