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

		override
		public IType GetIType()
		{
			return type;
		}

		override
		public IType GetIType(Context context)
        {
            return type;
        }

        public void setType(EnumeratedNativeType type)
        {
            this.type = type;
        }

        override
        public void ToDialect(CodeWriter writer)
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
			case Dialect.S:
				writer.append(" = ");
				expression.ToDialect(writer);
				break;
			}
		}

        override
        public bool Equals(Object obj)
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

        override
        public IType check(Context context)
        {
            IType actual = expression.check(context);
            if (!actual.isAssignableTo(context, type.getDerivedFrom()))
				throw new SyntaxError("Cannot assign " + actual.GetName() + " to " + type.getDerivedFrom().GetName());
            return type;
        }

        override
		public IValue interpret(Context context)
        {
            return expression.interpret(context);
        }

    }

}