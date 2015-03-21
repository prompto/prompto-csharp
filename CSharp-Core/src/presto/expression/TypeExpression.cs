using presto.type;
using System;
using presto.runtime;
using presto.parser;
using presto.value;
using presto.utils;


namespace presto.expression
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
