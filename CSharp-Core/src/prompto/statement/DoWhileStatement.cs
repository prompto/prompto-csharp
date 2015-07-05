using prompto.runtime;
using System;
using prompto.error;
using Boolean = prompto.value.Boolean;
using prompto.parser;
using prompto.type;
using prompto.expression;
using prompto.utils;
using prompto.value;

namespace prompto.statement
{

    public class DoWhileStatement : BaseStatement
    {

        IExpression condition;
        StatementList instructions;

        public DoWhileStatement(IExpression condition, StatementList instructions)
        {
            this.condition = condition;
            this.instructions = instructions;
        }

		override
		public void ToDialect(CodeWriter writer) {
			switch(writer.getDialect()) {
			case Dialect.E:
				toEDialect(writer);
				break;
			case Dialect.O:
				toODialect(writer);
				break;
			case Dialect.S:
				toPDialect(writer);
				break;
			}
		}

		private void toPDialect(CodeWriter writer) {
			toEDialect(writer);
		}

		private void toEDialect(CodeWriter writer) {
			writer.append("do:\n");
			writer.indent();
			instructions.ToDialect(writer);
			writer.dedent();
			writer.append("while ");
			condition.ToDialect(writer);
			writer.newLine();
		}

		private void toODialect(CodeWriter writer) {
			writer.append("do {\n");
			writer.indent();
			instructions.ToDialect(writer);
			writer.dedent();
			writer.append("} while (");
			condition.ToDialect(writer);
			writer.append(");\n");
		}      
        public IExpression getCondition()
        {
            return condition;
        }

        public StatementList getInstructions()
        {
            return instructions;
        }

        override
        public IType check(Context context)
        {
            IType cond = condition.check(context);
            if (cond != BooleanType.Instance)
                throw new SyntaxError("Expected a Boolean condition!");
            Context child = context.newChildContext();
            return instructions.check(child);
        }

        override
        public IValue interpret(Context context)
        {
			do {
				Context child = context.newChildContext ();
				IValue value = instructions.interpret (child);
				if (value != null)
					return value;
			} while(interpretCondition (context));
			return null;
        }

		bool interpretCondition(Context context)
		{
			IValue value = condition.interpret(context);
			if (!(value is Boolean))
				throw new InvalidDataError("Expected a Boolean, got:" + value.GetType().Name);
			return Boolean.TRUE == value;
		}

    }


}