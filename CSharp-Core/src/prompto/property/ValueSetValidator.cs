using System;
using System.Collections.Generic;
using prompto.error;
using prompto.jsx;
using prompto.runtime;
using prompto.type;

namespace prompto.property
{
    public class ValueSetValidator : BasePropertyValidator
    {
		HashSet<String> values;

		public ValueSetValidator(HashSet<String> values)
		{
			this.values = values;
		}

    	public override IType GetIType(Context context)
		{
			return AnyType.Instance;
		}

    	public override void Validate(Context context, JsxProperty property)
		{
			IJsxValue value = property.GetValue();
			if (value.IsLiteral())
			{
				String text = value.ToString();
				if (text.StartsWith("\"") && text.EndsWith("\""))
					text = text.Substring(1, text.Length - 1);
				if (!values.Contains(text))
                    throw new SyntaxError("Illegal value " + value.ToString());
			}
		}

	}
}
