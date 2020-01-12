using System;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.jsx
{

	public class JsxLiteral : IJsxValue
	{

		String text;

		public JsxLiteral(String text)
		{
			this.text = text;
		}

		public IType check(Context context)
		{
			return TextType.Instance;
		}

        public IType checkProto(Context context, MethodType type)
        {
			return VoidType.Instance;
		}

        public bool IsLiteral()
        {
            return true;
        }

        public void ToDialect(CodeWriter writer)
		{
			writer.append(text);
		}


	}
}