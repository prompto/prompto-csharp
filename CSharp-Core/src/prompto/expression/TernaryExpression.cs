using prompto.runtime;
using prompto.error;
using System;
using prompto.parser;
using prompto.type;
using prompto.utils;
using BooleanValue = prompto.value.BooleanValue;
using prompto.value;

namespace prompto.expression
{

    public class TernaryExpression : BaseExpression, IExpression
    {
        IExpression condition;
        IExpression ifTrue;
        IExpression ifFalse;

        public TernaryExpression(IExpression condition, IExpression ifTrue, IExpression ifFalse)
        {
            this.condition = condition;
            this.ifTrue = ifTrue;
            this.ifFalse = ifFalse;
        }

        public override IType check(Context context)
        {
            IType type = condition.check(context);
            if (!(type is BooleanType))
				throw new SyntaxError("Cannot test condition on " + type.GetTypeName());
            TypeMap types = new TypeMap();
            types.add(ifTrue.check(context));
            types.add(ifFalse.check(context));
            return types.inferType(context);
        }

        public override IValue interpret(Context context)
		{
			IValue test = condition.interpret(context);
			if(test == BooleanValue.TRUE)
				return ifTrue.interpret(context);
			else
				return ifFalse.interpret(context);
        }

        public override void ToDialect(CodeWriter writer)
		{
			if(writer.getDialect()==Dialect.O) {
				condition.ToDialect(writer);
				writer.append(" ? ");
				ifTrue.ToDialect(writer);
				writer.append(" : ");
				ifFalse.ToDialect(writer);
			} else {
				ifTrue.ToDialect(writer);
				writer.append(" if ");
				condition.ToDialect(writer);
				writer.append(" else ");
				ifFalse.ToDialect(writer);
			}
        }

    }

}
