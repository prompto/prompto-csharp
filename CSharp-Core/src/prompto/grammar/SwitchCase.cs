using prompto.parser;
using prompto.runtime;
using System;
using prompto.expression;
using prompto.statement;
using prompto.type;
using System.Collections.Generic;
using prompto.utils;
using prompto.value;

namespace prompto.grammar
{

    public abstract class SwitchCase : Section, ISection
    {

        protected IExpression expression;
        protected StatementList statements;

		public SwitchCase(IExpression expression, StatementList statements)
        {
            this.expression = expression;
			this.statements = statements;
        }

        public abstract void checkSwitchType(Context context, IType type);

        public IType checkReturnType(Context context)
        {
			return statements.check(context);
        }

        public abstract bool matches(Context context, Object value);

		public IValue interpret(Context context)
        {
			return statements.interpret(context);
        }

		public abstract void caseToEDialect(CodeWriter writer);
		public abstract void caseToODialect(CodeWriter writer);
		public abstract void caseToPDialect(CodeWriter writer);
		public abstract void catchToEDialect(CodeWriter writer);
		public abstract void catchToODialect(CodeWriter writer);
		public abstract void catchToPDialect(CodeWriter writer);

    }

    public class SwitchCaseList : List<SwitchCase> {

		public SwitchCaseList() {
		}

		public SwitchCaseList(SwitchCase item) {
			this.Add(item);
		}

	}

}