using System;
using prompto.parser;
using prompto.runtime;
using prompto.utils;
using prompto.error;
using prompto.type;
using prompto.declaration;
using prompto.value;
using prompto.grammar;


namespace prompto.argument
{

    public class CategoryArgument : BaseArgument, ITypedArgument
    {

        protected IType type;

        public CategoryArgument(IType type, String name)
            : base(name)
        {
            this.type = type;
        }

        public IType getType()
        {
            return type;
        }

        override
        public String getSignature(Dialect dialect)
        {
            return getProto();
        }

        override
        public String getProto(Context context)
        {
            return getProto();
        }

        String getProto()
        {
            return type.GetName();
        }

        override
       	public void ToDialect(CodeWriter writer)
        {
			if(this.mutable)
				writer.append("mutable ");
			switch(writer.getDialect()) {
			case Dialect.E:
				toEDialect(writer);
				break;
			case Dialect.O:
				toODialect(writer);
				break;
			case Dialect.S:
				toPDialect(writer);
				break;
			}
			if(DefaultValue!=null) {
				writer.append(" = ");
				DefaultValue.ToDialect(writer);
			}
		}

		protected virtual void toEDialect(CodeWriter writer) {
			bool anonymous = "any"==type.GetName();
			type.ToDialect(writer);
			if(anonymous) {
				writer.append(' ');
				writer.append(name);
			}
			if(!anonymous) {
				writer.append(' ');
				writer.append(name);
			}
		}

		protected virtual void toODialect(CodeWriter writer) {
			type.ToDialect(writer);
			writer.append(' ');
			writer.append(name);
		}

		protected virtual void toPDialect(CodeWriter writer) {
			writer.append(name);
			writer.append(':');
			type.ToDialect(writer);
		}

        override
        public bool Equals(Object obj)
        {
            if (obj == this)
                return true;
            if (obj == null)
                return false;
            if (!(obj is CategoryArgument))
                return false;
            CategoryArgument other = (CategoryArgument)obj;
            return Utils.equal(this.getType(), other.getType())
				&& Utils.equal(this.GetName(), other.GetName());
        }

        override
        public void register(Context context)
        {
			INamed actual = context.getRegisteredValue<INamed>(name);
            if (actual != null)
                throw new SyntaxError("Duplicate argument: \"" + name + "\"");
            context.registerValue(this);
			if (DefaultValue != null) {
				IValue value = DefaultValue.interpret (context);
				context.setValue (name, value);
			}
        }

        override
        public void check(Context context)
        {
            type.checkExists(context);
        }

        override
        public IType GetIType(Context context)
        {
            return type;
        }

    }
}