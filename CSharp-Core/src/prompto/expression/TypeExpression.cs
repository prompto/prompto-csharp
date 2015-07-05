using prompto.type;
using System;
using prompto.runtime;
using prompto.parser;
using prompto.value;
using prompto.utils;


namespace prompto.expression
{

    public class TypeExpression : IExpression
    {

        IType type;

        public TypeExpression(IType type)
        {
            this.type = type;
        }

		public IType getType()
		{
			return type;
		}

		public override string ToString()
		{
			return type.ToString ();
		}

        public void ToDialect(CodeWriter writer)
        {
			type.ToDialect(writer);
        }

          public IType check(Context context)
        {
            return this.type;
        }

		public IValue interpret(Context context)
        {
            return new TypeValue(type);
        }

		public IValue getMember(Context context, String name)
        {
            return type.getMember(context, name);
        }
    }
}
