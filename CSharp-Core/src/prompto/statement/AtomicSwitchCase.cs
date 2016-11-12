using prompto.runtime;
using System;
using prompto.error;
using Boolean = prompto.value.Boolean;
using prompto.statement;
using prompto.expression;
using prompto.type;
using prompto.utils;

namespace prompto.statement
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
            if (!type.isAssignableFrom(context, thisType))
				throw new SyntaxError("Cannot assign:" + thisType.GetTypeName() + " to:" + type.GetTypeName());

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