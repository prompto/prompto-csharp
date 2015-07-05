using System;
using System.Text;
using prompto.value;
using prompto.runtime;
using prompto.expression;
namespace prompto.literal
{


    public class DictEntry : BaseValue
    {

        IExpression key;
        IExpression value;


        public DictEntry(IExpression key, IExpression value)
			: base(null) // TODO check that this is safe
        {
            this.key = key;
            this.value = value;
        }

        override
        public IValue GetMember(Context context, String name)
        {
            if ("key" == name)
                return (IValue)key.interpret(context);
            else if ("value" == name)
                return (IValue)value.interpret(context);
            else
                throw new NotSupportedException("No such member:" + name);
        }
        
        public IExpression getKey()
        {
            return key;
        }

        public IExpression getValue()
        {
            return value;
        }

        override
        public String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append( key.ToString());
            sb.Append(":");
            sb.Append(value.ToString());
            return sb.ToString();
        }

    }

}
