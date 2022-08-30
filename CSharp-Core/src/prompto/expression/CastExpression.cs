using System;
using prompto.type;
using prompto.runtime;
using prompto.error;
using prompto.utils;
using prompto.parser;
using prompto.value;
using prompto.declaration;

namespace prompto.expression
{

    public class CastExpression : BaseExpression, IExpression
    {

        IExpression expression;
        IType type;
        bool mutable;

        public CastExpression(IExpression expression, IType type, bool mutable)
        {
            this.expression = expression;
            this.type = type;
            this.mutable = mutable;
        }

        public override IType check(Context context)
        {
            IType actual = expression.check(context).Anyfy();
            IType target = getTargetType(context, type, mutable);
            // check any
            if (actual == AnyType.Instance)
                return target;
            // check upcast
            if (target.isAssignableFrom(context, actual))
                return target;
            // check downcast
            if (actual.isAssignableFrom(context, target))
                return target;
            throw new SyntaxError("Cannot cast " + actual.ToString() + " to " + target.ToString());
        }

        private static IType getTargetType(Context context, IType type, bool mutable)
        {
            if (type is IterableType)
            {
                IType itemType = getTargetType(context, ((IterableType)type).GetItemType(), false);
                return ((IterableType)type).WithItemType(itemType).AsMutable(context, mutable);
            }
            else if (type is NativeType)
                return type;
            else
                return getTargetAtomicType(context, type, mutable);
        }


        private static IType getTargetAtomicType(Context context, IType type, bool mutable)
        {
            IDeclaration decl = context.getRegisteredDeclaration<IDeclaration>(type.GetTypeName());
            if (decl == null)
                throw new SyntaxError("Unknown identifier: " + type.GetTypeName());
            else if (decl is MethodDeclarationMap)
            {
                MethodDeclarationMap map = (MethodDeclarationMap)decl;
                if (map.Count == 1)
                    return new MethodType(map.GetFirst());
                else
                    throw new SyntaxError("Ambiguous identifier: " + type.GetTypeName());
            }
            else
                return decl.GetIType(context).AsMutable(context, mutable);
        }


        public override IValue interpret(Context context)
        {
            IValue value = expression.interpret(context);
            if (value != NullValue.Instance)
            {
                IType target = getTargetType(context, type, mutable);
                if (!target.Equals(value.GetIType()))
                {
                    if (target == DecimalType.Instance && value is IntegerValue)
                        value = new value.DecimalValue(((IntegerValue)value).DoubleValue);
                    else if (target == IntegerType.Instance && value is value.DecimalValue)
                        value = new IntegerValue(((value.DecimalValue)value).LongValue);
                    else if(value.GetIType().isAssignableFrom(context, target))
                        value.SetIType(target);
                    else if(!target.isAssignableFrom(context, value.GetIType()))
                        throw new SyntaxError("Cannot cast " + value.GetIType().ToString() + " to " + target.ToString());
                }
            }
            return value;
        }

        public override void ToDialect(CodeWriter writer)
        {
            switch (writer.getDialect())
            {
                case Dialect.E:
                case Dialect.M:
                    expression.ToDialect(writer);
                    writer.append(" as ");
                    if (mutable)
                        writer.append("mutable ");
                    type.ToDialect(writer);
                    break;
                case Dialect.O:
                    writer.append("(");
                    if (mutable)
                        writer.append("mutable ");
                    type.ToDialect(writer);
                    writer.append(")");
                    expression.ToDialect(writer);
                    break;
            }

        }

    }

}