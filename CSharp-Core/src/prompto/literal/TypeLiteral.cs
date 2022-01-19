using System;
using prompto.declaration;
using prompto.error;
using prompto.expression;
using prompto.parser;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.literal
{
    public class TypeLiteral : Section, IExpression
    {
        IType type;

        public TypeLiteral(IType type)
        {
            this.type = type;
        }

        public IType getType()
        {
            return type;
        }


        public IType check(Context context)
        {
            return new TypeType(type);
        }


        public virtual IType checkReference(Context context)
        {
            return check(context);
        }

        public AttributeDeclaration CheckAttribute(Context context)
        {
            throw new SyntaxError("Expected an attribute, found: " + this.ToString());
        }

        public IValue interpret(Context context)
        {
            return new TypeValue(type);
        }

        public virtual IValue interpretReference(Context context)
        {
            return interpret(context);
        }

        public void ToDialect(CodeWriter writer)
        {
            if (writer.getDialect() == Dialect.E)
            {
                IDeclaration decl = writer.getContext().getRegisteredDeclaration<IDeclaration>(type.GetTypeName());
                if(decl is MethodDeclarationMap)
                    writer.append("Method: ");
                else
                    writer.append("Type: ");
            }
            type.ToDialect(writer);
        }


        public void ParentToDialect(CodeWriter writer)
        {
            type.ToDialect(writer);
        }


        public override String ToString()
        {
            return type.ToString();
        }
    }
}
