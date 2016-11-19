using System;
using prompto.grammar;
using prompto.type;
using prompto.utils;
using prompto.runtime;
using prompto.error;
using prompto.declaration;
using prompto.value;

namespace prompto.argument
{
	public class ExtendedArgument : CategoryArgument
	{
		IdentifierList attributes;

		public ExtendedArgument (IType type, String name)
			: base (type, name)
		{
		}

		public ExtendedArgument (IType type, String name, IdentifierList attributes)
			: base (type, name)
		{
			this.attributes = attributes;
		}

		public void setAttributes (IdentifierList attributes)
		{
			this.attributes = attributes;
		}

		public bool hasAttributes()
		{
			return attributes != null;
		}

		public IdentifierList getAttributes()
		{
			return attributes;
		}

		public override String getProto() {
			return type.GetTypeName() + '(' + attributes.ToString() + ')';
		}

		protected override void ToEDialect(CodeWriter writer) {
			type.ToDialect(writer);
			writer.append(' ');
			writer.append(name);
			if(attributes!=null) {
				switch(attributes.Count) {
				case 0:
					break;
				case 1:
					writer.append(" with attribute ");
					attributes.ToDialect(writer, true);
					break;
				default:
					writer.append(" with attributes ");
					attributes.ToDialect(writer, true);
					break;
				}
			}
		}

		protected override void ToODialect(CodeWriter writer) {
			type.ToDialect(writer);
			if(attributes!=null) {
				writer.append('(');
				attributes.ToDialect(writer, true);
				writer.append(')');
			}
			writer.append(' ');
			writer.append(name);
		}

		protected override void toPDialect(CodeWriter writer) {
			writer.append(name);
			writer.append(':');
			type.ToDialect(writer);
			if(attributes!=null) {
				writer.append('(');
				attributes.ToDialect(writer, true);
				writer.append(')');
			}
		}


		public override bool Equals(Object obj)
		{
			if (obj == this)
				return true;
			if (obj == null)
				return false;
			if (!(obj is ExtendedArgument))
				return false;
			ExtendedArgument other = (ExtendedArgument)obj;
			return ObjectUtils.equal(this.getType(), other.getType())
				&& ObjectUtils.equal(this.GetName(), other.GetName())
				&& ObjectUtils.equal(this.getAttributes(), other.getAttributes());
		}


		public override void register(Context context)
		{
			INamed actual = context.getRegisteredValue<INamed>(name);
			if (actual != null)
				throw new SyntaxError("Duplicate argument: \"" + name + "\"");
			ConcreteCategoryDeclaration declaration = new ConcreteCategoryDeclaration(name);
			declaration.setDerivedFrom(new IdentifierList(type.GetTypeName()));
			declaration.setAttributes(attributes);
			context.registerDeclaration(declaration);
			context.registerValue(this);
			if (DefaultValue != null) {
				IValue value = DefaultValue.interpret (context);
				context.setValue (name, value);
			}
		}


		public override void check(Context context)
		{
			type.checkExists(context);
			foreach (String attribute in attributes)
			{
				AttributeDeclaration actual = context.getRegisteredDeclaration<AttributeDeclaration>(attribute);
				if (actual == null)
					throw new SyntaxError("Unknown attribute: \"" + attribute + "\"");
			}
		}


		public override IType GetIType(Context context)
		{
			return context.getRegisteredDeclaration<IDeclaration>(name).GetIType(context);
		}

	}
}

