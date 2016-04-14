using System;
using prompto.runtime;
using prompto.parser;
using prompto.type;
using prompto.grammar;
using prompto.expression;
using prompto.utils;
using prompto.value;
using prompto.constraint;

namespace prompto.declaration
{

    public class AttributeDeclaration : BaseDeclaration
    {

        IType type;
        IAttributeConstraint constraint;

        public AttributeDeclaration(String name, IType type)
            : base(name)
        {
            this.type = type;
        }

        public AttributeDeclaration(String name, IType type, IAttributeConstraint constraint)
            : base(name)
        {
            this.type = type;
            this.constraint = constraint;
        }

		public bool Storable { get; set; }
	
        public IType getType()
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
				writer.append(';');
				break;
			case Dialect.S:
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
				else
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
    }

}
