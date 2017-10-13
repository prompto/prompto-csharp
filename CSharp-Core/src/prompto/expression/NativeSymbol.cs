using prompto.runtime;
using System;
using prompto.error;
using prompto.parser;
using prompto.type;
using prompto.expression;
using prompto.utils;
using prompto.value;


namespace prompto.expression
{

    public class NativeSymbol : Symbol, IExpression
    {

        EnumeratedNativeType type;
        IExpression expression;

        public NativeSymbol(String name, IExpression expression)
            : base(name)
        {
            this.expression = expression;
        }

        public IExpression getExpression()
        {
            return expression;
        }


		public override IType GetIType()
		{
			return type;
		}

		public override IType GetIType(Context context)
        {
            return type;
        }

		public override void SetIType(IType type)
        {
			this.type = (EnumeratedNativeType)type;
        }

        
        public override void ToDialect(CodeWriter writer)
        {
			writer.append(symbol);
			switch(writer.getDialect()) {
			case Dialect.E:
				writer.append(" with ");
				expression.ToDialect(writer);
				writer.append(" as value");
				break;
			case Dialect.O:
				writer.append(" = ");
				expression.ToDialect(writer);
				break;
			case Dialect.M:
				writer.append(" = ");
				expression.ToDialect(writer);
				break;
			}
		}

        
        public override bool Equals(Object obj)
        {
            if (obj == this)
                return true;
            if (obj == null)
                return false;
            if (!(obj is NativeSymbol))
                return false;
            NativeSymbol other = (NativeSymbol)obj;
			return this.GetName().Equals(other.GetName())
                    && this.getExpression().Equals(other.getExpression());
        }

        
        public override IType check(Context context)
        {
            IType actual = expression.check(context);
            if (!type.getDerivedFrom().isAssignableFrom(context, actual))
				throw new SyntaxError("Cannot assign " + actual.GetTypeName() + " to " + type.getDerivedFrom().GetTypeName());
            return type;
        }

        override
		public IValue interpret(Context context)
        {
            return this;
        }


	public override IValue GetMember(Context context, String name, bool autoCreate) 
	{
		if("name".Equals(name))
			return new Text(this.GetName());
		else if("value".Equals(name))
			return expression.interpret(context);
		else
			return base.GetMember(context, name, autoCreate);
	}

    }

}