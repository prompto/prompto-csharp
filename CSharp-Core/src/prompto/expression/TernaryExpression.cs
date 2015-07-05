using prompto.runtime;
using prompto.error;
using System;
using prompto.parser;
using prompto.type;
using prompto.utils;
using Boolean = prompto.value.Boolean;
using prompto.value;

namespace prompto.expression
{

    public class TernaryExpression : IExpression
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

        public IType check(Context context)
        {
            IType type = condition.check(context);
            if (!(type is BooleanType))
				throw new SyntaxError("Cannot test condition on " + type.GetName());
            IType trueType = ifTrue.check(context);
            // IType falseType = ifFalse.check(context);
            // TODO check compatibility
            return trueType;
        }

        public IValue interpret(Context context)
		{
			IValue test = condition.interpret(context);
			if(test == Boolean.TRUE)
				return ifTrue.interpret(context);
			else
				return ifFalse.interpret(context);
        }

        public void ToDialect(CodeWriter writer)
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
