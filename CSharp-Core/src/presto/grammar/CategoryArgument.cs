using System;
using presto.parser;
using presto.runtime;
using presto.utils;
using presto.error;
using presto.type;
using presto.declaration;
using presto.value;


namespace presto.grammar
{

    public class CategoryArgument : BaseArgument, ITypedArgument
    {

        IType type;
        IdentifierList attributes;

        public CategoryArgument(IType type, String name)
            : base(name)
        {
            this.type = type;
        }

        public CategoryArgument(IType type, String name, IdentifierList attributes)
            : base(name)
        {
            this.type = type;
            this.attributes = attributes;
        }

        public void setAttributes(IdentifierList attributes)
        {
            this.attributes = attributes;
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
            if (attributes == null)
                return type.getName();
            else
                return type.getName() + '(' + attributes.ToString() + ')';
        }

        override
       	public void ToDialect(CodeWriter writer)
        {
			switch(writer.getDialect()) {
			case Dialect.E:
				toEDialect(writer);
				break;
			case Dialect.O:
				toODialect(writer);
				break;
			case Dialect.P:
				toPDialect(writer);
				break;
			}
			if(DefaultValue!=null) {
				writer.append(" = ");
				DefaultValue.ToDialect(writer);
			}
		}

		private void toEDialect(CodeWriter writer) {
			bool anonymous = "any"==type.getName();
			type.ToDialect(writer);
			if(anonymous) {
				writer.append(' ');
				writer.append(name);
			}
			if(attributes!=null) {
				switch(attributes.Count) {
				case 0:
					break;
				case 1:
					writer.append(" with attribute: ");
					attributes.ToDialect(writer, true);
					break;
				default:
					writer.append(" with attribute: ");
					attributes.ToDialect(writer, true);
					break;
				}
			}
			if(!anonymous) {
				writer.append(' ');
				writer.append(name);
			}
		}

		private void toODialect(CodeWriter writer) {
			type.ToDialect(writer);
			if(attributes!=null) {
				writer.append('(');
				attributes.ToDialect(writer, true);
				writer.append(')');
			}
			writer.append(' ');
			writer.append(name);
		}

		private void toPDialect(CodeWriter writer) {
			writer.append(name);
			writer.append(':');
			type.ToDialect(writer);
			if(attributes!=null) {
				writer.append('(');
				attributes.ToDialect(writer, true);
				writer.append(')');
			}
		}
        public bool hasAttributes()
        {
            return attributes != null;
        }

        public IdentifierList getAttributes()
        {
            return attributes;
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
                    && Utils.equal(this.getName(), other.getName())
                    && Utils.equal(this.getAttributes(), other.getAttributes());
        }

        override
        public void register(Context context)
        {
			INamed actual = context.getRegisteredValue<INamed>(name);
            if (actual != null)
                throw new SyntaxError("Duplicate argument: \"" + name + "\"");
            if (attributes != null)
            {
                ConcreteCategoryDeclaration declaration = new ConcreteCategoryDeclaration(name);
                declaration.setDerivedFrom(new IdentifierList(type.getName()));
                declaration.setAttributes(attributes);
                context.registerDeclaration(declaration);
            }
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
            if (attributes != null) foreach (String attribute in attributes)
                {
                    AttributeDeclaration actual = context.getRegisteredDeclaration<AttributeDeclaration>(attribute);
                    if (actual == null)
                        throw new SyntaxError("Unknown attribute: \"" + attribute + "\"");
                }
        }

        override
        public IType GetType(Context context)
        {
            if (attributes == null)
                return type;
            else
                return context.getRegisteredDeclaration<IDeclaration>(name).GetType(context);
        }

    }
}
