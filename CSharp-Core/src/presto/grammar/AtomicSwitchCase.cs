using presto.runtime;
using System;
using presto.error;
using Boolean = presto.value.Boolean;
using presto.statement;
using presto.expression;
using presto.type;
using presto.utils;

namespace presto.grammar
{

    public class AtomicSwitchCase : SwitchCase
    {

        public AtomicSwitchCase(IExpression expression, StatementList list)
            : base(expression, list)
        {
        }

        override
        public void checkSwitchType(Context context, IType type)
        {
            IType thisType = expression.check(context);
            if (!thisType.isAssignableTo(context, type))
                throw new SyntaxError("Cannot assign:" + thisType.getName() + " to:" + type.getName());

        }

        override
        public bool matches(Context context, Object value)
        {
            Object thisValue = expression.interpret(context);
            return value.Equals(thisValue);
        }

		override
		public void caseToPDialect(CodeWriter writer) {
			caseToEDialect(writer);
		}

		override
		public void caseToODialect(CodeWriter writer) {
			writer.append("case ");
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
		public void caseToEDialect(CodeWriter writer) {
			writer.append("when ");
			expression.ToDialect(writer);
			writer.append(":\n");
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
		}

		override
		public void catchToPDialect(CodeWriter writer) {
			writer.append("except ");
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