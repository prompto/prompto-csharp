using presto.runtime;
using presto.error;
using System;
using Boolean = presto.value.Boolean;
using presto.value;
using presto.statement;
using presto.expression;
using presto.type;
using presto.utils;

namespace presto.grammar
{

    public class CollectionSwitchCase : SwitchCase
    {

        public CollectionSwitchCase(IExpression expression, StatementList list)
            : base(expression, list)
        {
        }

        override
        public void checkSwitchType(Context context, IType type)
        {
            IType thisType = expression.check(context);
            if (thisType is CollectionType)
                thisType = ((CollectionType)thisType).GetItemType();
            if (!thisType.isAssignableTo(context, type))
                throw new SyntaxError("Cannot assign:" + thisType.getName() + " to:" + type.getName());
        }

        override
        public bool matches(Context context, Object value)
        {
            Object thisValue = expression.interpret(context);
            if (value is IExpression)
                value = ((IExpression)value).interpret(context);
            if (thisValue is IContainer)
                return ((IContainer)thisValue).HasItem(context, (IValue)value);
            else 
                return false;
        }

		override
		public void caseToPDialect(CodeWriter writer) {
			caseToEDialect(writer);
		}

		override
		public void caseToODialect(CodeWriter writer) {
			writer.append("case in ");
			expression.ToDialect(writer);
			writer.append(":\n");
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
		}

		override
		public void caseToEDialect(CodeWriter writer) {
			writer.append("when in ");
			expression.ToDialect(writer);
			writer.append(":\n");
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
		}

		override
		public void catchToODialect(CodeWriter writer) {
			writer.append("catch (");
			expression.ToDialect(writer);
			writer.append(") {\n");
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
			writer.append("} ");
		}

		override
		public void catchToPDialect(CodeWriter writer) {
			writer.append("except in ");
			expression.ToDialect(writer);
			writer.append(":\n");
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
		}

		override
		public void catchToEDialect(CodeWriter writer) {
			caseToEDialect(writer); // no difference
		}
    }

}
