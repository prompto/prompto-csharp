using System;
using prompto.runtime;
using prompto.type;
using prompto.utils;

namespace prompto.jsx {

	public class JsxProperty
	{

		String name;
		IJsxValue value;
		String suite;

		public JsxProperty(String name, IJsxValue value, String suite)
		{
			this.name = name;
			this.value = value;
        	this.suite = (suite != null && suite.Length>0) ? suite : null;
		}

        public IJsxValue GetValue()
        {
			return value;
        }

		public IType check(Context context)
		{
			if (value != null)
				return value.check(context);
			else
				return VoidType.Instance;
		}

		public IType checkProto(Context context, MethodType type)
		{
			if (value != null)
				return value.checkProto(context, type);
			else
				return VoidType.Instance;
		}

		public void ToDialect(CodeWriter writer)
		{
			writer.append(name);
			if (value != null)
			{
				writer.append("=");
				value.ToDialect(writer);
			}
			if(suite!=null)
				writer.appendRaw(suite);
			else
				writer.append(" ");
		}

	}

}
