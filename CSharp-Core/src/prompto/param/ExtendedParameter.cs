using System;
using prompto.grammar;
using prompto.type;
using prompto.utils;
using prompto.runtime;
using prompto.error;
using prompto.declaration;
using prompto.value;

namespace prompto.param
{
	public class ExtendedParameter : CategoryParameter
	{
		IdentifierList attributes;

		public ExtendedParameter (IType type, String name)
			: base (type, name)
		{
		}

		public ExtendedParameter (IType type, String name, IdentifierList attributes)
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

		protected override void ToMDialect(CodeWriter writer) {
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
			if (!(obj is ExtendedParameter))
				return false;
			ExtendedParameter other = (ExtendedParameter)obj;
			return ObjectUtils.AreEqual(this.getType(), other.getType())
				&& ObjectUtils.AreEqual(this.GetName(), other.GetName())
				&& ObjectUtils.AreEqual(this.getAttributes(), other.getAttributes());
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


		public override IType check(Context context)
		{
			type.checkExists(context);
			foreach (String attribute in attributes)
			{
				AttributeDeclaration actual = context.getRegisteredDeclaration<AttributeDeclaration>(attribute);
				if (actual == null && attribute != "text")
					throw new SyntaxError("Unknown attribute: \"" + attribute + "\"");
			}
			return type;
		}


		public override IType GetIType(Context context)
		{
			return context.getRegisteredDeclaration<IDeclaration>(name).GetIType(context);
		}

	}
}

