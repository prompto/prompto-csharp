using System;
using System.Text;
using prompto.value;
using prompto.runtime;
using prompto.expression;
using prompto.utils;

namespace prompto.literal
{


	public class DictEntry : BaseValue
	{

		DictKey key;
		IExpression value;


		public DictEntry(DictKey key, IExpression value)
			: base(null) // TODO check that this is safe
		{
			this.key = key;
			this.value = value;
		}


		public override IValue GetMember(Context context, String name, bool autoCreate)
		{
			if ("key" == name)
				return key.asText();
			else if ("value" == name)
				return (IValue)value.interpret(context);
			else
				throw new NotSupportedException("No such member:" + name);
		}

		public DictKey getKey()
		{
			return key;
		}

		public void ToDialect(CodeWriter writer)
		{
			key.ToDialect(writer);
			writer.append(':');
			value.ToDialect(writer);
		}

		public IExpression getValue()
		{
			return value;
		}


		public override String ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(key.ToString());
			sb.Append(":");
			sb.Append(value.ToString());
			return sb.ToString();
		}

	}

}
