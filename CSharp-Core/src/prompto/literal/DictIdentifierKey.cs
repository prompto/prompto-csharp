using System;
using prompto.error;
using prompto.expression;
using prompto.runtime;
using prompto.utils;
using prompto.value;

namespace prompto.literal
{
	public class DictIdentifierKey : DictKey
	{
		String id;

		public DictIdentifierKey(String id)
		{
			this.id = id;
		}

        internal override void ToDialect(CodeWriter writer)
        {
            writer.append(id);
        }

    	public override string ToString()
		{
			return this.id;
		}

        internal override string interpret(Context context)
        {
            IValue value = new InstanceExpression(id).interpret(context);
            if (value is TextValue)
                return value.ToString();
            else
                throw new SyntaxError("Expected a Text, got a " + value.GetIType().GetTypeName());
        }
    }
}
