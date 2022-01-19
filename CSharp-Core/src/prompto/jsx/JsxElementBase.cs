using System;
using System.Collections.Generic;
using prompto.declaration;
using prompto.error;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.jsx
{

	public abstract class JsxElementBase : IJsxExpression
	{

		protected String name;
		protected List<JsxProperty> properties;


		public JsxElementBase(String name, List<JsxProperty> properties)
		{
			this.name = name;
			this.properties = properties;
		}

		public IType check(Context context)
		{
			if (properties != null)
			{
				foreach (JsxProperty prop in properties)
				{
					prop.check(context);
				}
			}
			return JsxType.Instance;
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

		public abstract void ToDialect(CodeWriter writer);

        public void ParentToDialect(CodeWriter writer)
        {
            ToDialect(writer);
        }

    }
}