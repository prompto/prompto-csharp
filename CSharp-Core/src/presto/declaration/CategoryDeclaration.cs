using System;
using presto.error;
using presto.runtime;
using presto.type;
using presto.grammar;
using presto.value;
using presto.utils;
using presto.parser;

namespace presto.declaration
{

	public abstract class CategoryDeclaration : BaseDeclaration
	{
	
		protected IdentifierList attributes;

		public CategoryDeclaration (String name)
			: base (name)
		{
		}

		public CategoryDeclaration (String name, IdentifierList attributes)
			: base (name)
		{
			this.attributes = attributes;
		}

		public void setAttributes (IdentifierList attributes)
		{
			this.attributes = attributes;
		}

		public IdentifierList getAttributes ()
		{
			return attributes;
		}


		public override void register (Context context)
		{
			context.registerDeclaration (this);
		}


		public override IType check (Context context)
		{
			if (attributes != null)
				foreach (String attribute in attributes) {
					AttributeDeclaration ad = context.getRegisteredDeclaration<AttributeDeclaration> (attribute);
					if (ad == null)
						throw new SyntaxError ("Unknown attribute: \"" + attribute + "\"");
				}
			return new CategoryType (this.getName ());
		}


		public override IType GetType (Context context)
		{
			return new CategoryType (name);
		}

		public virtual bool hasAttribute (Context context, String name)
		{
			return attributes != null && attributes.Contains (name);
		}

		public virtual bool hasMethod (Context context, String key, Object o)
		{
			return false;
		}

		public virtual bool isDerivedFrom (Context context, CategoryType categoryType)
		{
			return false;
		}

		public virtual IdentifierList getDerivedFrom ()
		{
			return null;
		}

		public abstract IInstance newInstance ();

		public virtual void checkConstructorContext (Context context)
		{
			// nothing to do
		}


		public override void ToDialect (CodeWriter writer)
		{
			switch (writer.getDialect ()) {
			case Dialect.E:
				toEDialect (writer);
				break;
			case Dialect.O:
				toODialect (writer);
				break;
			case Dialect.P:
				toPDialect (writer);
				break;
			}
		}

		protected abstract void toEDialect (CodeWriter writer);

		protected virtual void protoToEDialect (CodeWriter writer, bool hasMethods, bool hasMappings)
		{
			bool hasAttributes = attributes != null && attributes.Count > 0;
			writer.append ("define ");
			writer.append (name);
			writer.append (" as: ");
			categoryTypeToEDialect (writer);
			if (hasAttributes) {
				if (attributes.Count == 1)
					writer.append (" with attribute: ");
				else
					writer.append (" with attributes: ");
				attributes.ToDialect (writer, true);
			}
			if (hasMethods) {
				if (hasAttributes)
					writer.append (", and methods:");
				else
					writer.append (" with methods:");
			} else if (hasMappings) {
				if (hasAttributes)
					writer.append (", and mappings:");
				else
					writer.append (" with mappings:");
			}
			writer.newLine ();	
		}

		protected virtual void methodsToEDialect (CodeWriter writer, CategoryMethodDeclarationList methods)
		{
			writer.indent ();
			foreach (IDeclaration decl in methods) {
				writer.newLine ();
				decl.ToDialect (writer);
			}
			writer.dedent ();
		}


		protected abstract void categoryTypeToEDialect (CodeWriter writer);

		protected abstract void toODialect (CodeWriter writer);

		protected virtual void toODialect (CodeWriter writer, bool hasBody)
		{
			categoryTypeToODialect (writer);
			writer.append (" ");
			writer.append (name);
			if (attributes != null) {
				writer.append ('(');
				attributes.ToDialect (writer, true);
				writer.append (')');
			}	
			categoryExtensionToODialect (writer);
			if (hasBody) {
				writer.append (" {\n");
				writer.newLine ();
				writer.indent ();
				bodyToODialect (writer);
				writer.dedent ();
				writer.append ('}');
				writer.newLine ();
			} else
				writer.append (';');
		}

		protected abstract void categoryTypeToODialect (CodeWriter writer);

		protected virtual void categoryExtensionToODialect (CodeWriter writer)
		{
			// by default no extension
		}

		protected abstract void bodyToODialect (CodeWriter writer);

		protected abstract void toPDialect (CodeWriter writer);

		protected void protoToPDialect (CodeWriter writer, IdentifierList derivedFrom)
		{
			categoryTypeToPDialect (writer);
			writer.append (" ");
			writer.append (name);
			writer.append ("(");
			if (derivedFrom != null) {
				derivedFrom.ToDialect (writer, false);
				if (attributes != null)
					writer.append (", ");
			}
			if (attributes != null)
				attributes.ToDialect (writer, false);
			writer.append ("):\n");
			writer.newLine ();
		}

		protected abstract void categoryTypeToPDialect (CodeWriter writer);


	}

}