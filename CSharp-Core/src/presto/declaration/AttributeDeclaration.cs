using System;
using presto.runtime;
using presto.parser;
using presto.type;
using presto.grammar;
using presto.expression;
using presto.utils;
using presto.value;

namespace presto.declaration
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
				writer.append(" as: ");
				type.ToDialect(writer);
				writer.append(" attribute");
				if(constraint!=null)
					constraint.ToDialect(writer);
				break;
			case Dialect.O:
				writer.append("attribute ");
				writer.append(GetName());
				writer.append(" : ");
				type.ToDialect(writer);
				if(constraint!=null)
					constraint.ToDialect(writer);
				writer.append(';');
				break;
			case Dialect.S:
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
        public IType GetType(Context context)
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
