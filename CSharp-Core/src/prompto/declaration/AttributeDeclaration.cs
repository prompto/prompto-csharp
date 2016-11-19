using System;
using prompto.runtime;
using prompto.parser;
using prompto.type;
using prompto.grammar;
using prompto.expression;
using prompto.utils;
using prompto.value;
using prompto.constraint;
using System.Collections.Generic;
using prompto.store;

namespace prompto.declaration
{

    public class AttributeDeclaration : BaseDeclaration
    {

        IType type;
        IAttributeConstraint constraint;
		IdentifierList indexTypes;

        public AttributeDeclaration(String name, IType type)
            : base(name)
        {
            this.type = type;
        }

		public AttributeDeclaration(String name, IType type, IAttributeConstraint constraint, IdentifierList indexTypes)
            : base(name)
        {
            this.type = type;
            this.constraint = constraint;
			this.indexTypes = indexTypes;
        }

		public bool Storable { get; set; }
	
        public IType getIType()
        {
            return type;
        }

        public IAttributeConstraint getConstraint()
        {
            return constraint;
        }

        override
        public void ToDialect(CodeWriter writer)
        {
			switch(writer.getDialect()) {
			case Dialect.E:
				writer.append("define ");
				writer.append(GetName());
				writer.append(" as ");
				if(this.Storable)
					writer.append("storable ");
				type.ToDialect(writer);
				writer.append(" attribute");
				if(constraint!=null)
					constraint.ToDialect(writer);
				if(indexTypes!=null) {
					writer.append(" with ");
					indexTypes.ToDialect(writer, true);
					writer.append(" index");
				}
				break;
			case Dialect.O:
				if(this.Storable)
					writer.append("storable ");
				writer.append("attribute ");
				writer.append(GetName());
				writer.append(" : ");
				type.ToDialect(writer);
				if(constraint!=null)
					constraint.ToDialect(writer);
				if(indexTypes!=null) {
					writer.append(" with index");
					if(indexTypes.Count>0) {
						writer.append(" (");
						indexTypes.ToDialect(writer, false);
						writer.append(')');
					}
				}
				writer.append(';');
				break;
			case Dialect.M:
				if(this.Storable)
					writer.append("storable ");
				writer.append("attr ");
				writer.append(GetName());
				writer.append(" ( ");
				type.ToDialect(writer);
				writer.append(" ):\n");
				writer.indent();
				if(constraint!=null)
					constraint.ToDialect(writer);
				if(indexTypes!=null) {
					if(constraint!=null)
						writer.newLine();
					writer.append("index (");
					indexTypes.ToDialect(writer, false);
					writer.append(')');
				}
				if(constraint==null && indexTypes==null)
					writer.append("pass");
				writer.dedent();
				break;
			}
		}

        override
        public void register(Context context)
        {
            context.registerDeclaration(this);
        }

        override
        public IType check(Context context)
        {
            type.checkExists(context);
            return type;
        }

        override
        public IType GetIType(Context context)
        {
            return type;
        }

        public void setConstraint(IAttributeConstraint constraint)
        {
            this.constraint = constraint;
        }

		public IValue checkValue(Context context, IExpression expression)
        {
			IValue value = expression.interpret(context);
            if (constraint != null)
	            constraint.checkValue(context, value);
            return value;
        }

		public AttributeInfo getAttributeInfo()
		{
			List<String> list = indexTypes == null ? null : indexTypes;
			bool collection = type is ContainerType;
			TypeFamily family = collection ? ((ContainerType)type).GetItemType().GetFamily() : type.GetFamily();
			return new AttributeInfo(GetName(), family, collection, list);
		}

	}

}
