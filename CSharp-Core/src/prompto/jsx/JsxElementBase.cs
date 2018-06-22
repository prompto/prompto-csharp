using System;
using System.Collections.Generic;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.jsx
{

	public abstract class JsxElementBase : IJsxExpression
	{

		protected String name;
		protected List<JsxAttribute> attributes;


		public JsxElementBase(String name, List<JsxAttribute> attributes)
		{
			this.name = name;
			this.attributes = attributes;
		}

		public IType check(Context context)
		{
			if (attributes != null)
			{
				foreach (JsxAttribute attr in attributes)
				{
					attr.check(context);
				}
			}
			return JsxType.Instance;
		}

		public IValue interpret(Context context)
		{
			return new JsxValue(this);
		}

		public abstract void ToDialect(CodeWriter writer);

	}
}