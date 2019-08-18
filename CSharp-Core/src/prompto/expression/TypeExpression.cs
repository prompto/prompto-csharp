using prompto.type;
using System;
using prompto.runtime;
using prompto.value;
using prompto.utils;


namespace prompto.expression
{

    public class TypeExpression : BaseExpression, IExpression
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

        public override void ToDialect(CodeWriter writer)
        {
			type.ToDialect(writer);
        }

          public override IType check(Context context)
        {
            return new TypeType(type);
        }

		public override IValue interpret(Context context)
        {
            return new TypeValue(type);
        }

		public IValue getMember(Context context, String name)
        {
            return type.getStaticMemberValue(context, name);
        }
    }
}
