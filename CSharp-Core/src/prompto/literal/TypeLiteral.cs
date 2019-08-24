using System;
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


        public IValue interpret(Context context)
        {
            return new TypeValue(type);
        }


        public void ToDialect(CodeWriter writer)
        {
            if (writer.getDialect() == Dialect.E)
                writer.append("Type: ");
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
