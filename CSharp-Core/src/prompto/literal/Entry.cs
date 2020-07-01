using System;
using System.Text;
using prompto.value;
using prompto.runtime;
using prompto.expression;
using prompto.utils;

namespace prompto.literal
{


	public class Entry<K> : BaseValue where K : Key
	{

		K key;
		IExpression value;


		public Entry(K key, IExpression value)
			: base(null) // TODO check that this is safe
		{
			this.key = key;
			this.value = value;
		}


		public override IValue GetMemberValue(Context context, String name, bool autoCreate)
		{
			if ("key" == name)
				return new TextValue(key.interpret(context));
			else if ("value" == name)
				return value.interpret(context);
			else
				throw new NotSupportedException("No such member:" + name);
		}

		public K GetKey()
		{
			return key;
		}

		public void ToDialect(CodeWriter writer)
		{
			key.ToDialect(writer);
			writer.append(':');
			value.ToDialect(writer);
		}

		public IExpression GetValue()
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
