using prompto.runtime;
using System;
using prompto.type;
using prompto.expression;
using prompto.grammar;
using prompto.utils;
using prompto.value;


namespace prompto.statement
{

    public class SwitchStatement : BaseSwitchStatement
    {

        IExpression expression;

        public SwitchStatement(IExpression expression)
        {
            this.expression = expression;
        }

        public SwitchStatement(IExpression expression, SwitchCaseList switchCases, StatementList defaultCase)
            : base(switchCases, defaultCase)
        {
            this.expression = expression;
        }

		override
		protected void toODialect(CodeWriter writer) {
			writer.append("switch(");
			expression.ToDialect(writer);
			writer.append(") {\n");
			foreach(SwitchCase sc in switchCases)
				sc.caseToODialect(writer);
			if(defaultCase!=null) {
				writer.append("default:\n");
				writer.indent();
				defaultCase.ToDialect(writer);
				writer.dedent();
			}
			writer.append("}\n");
		}

		override
		protected void toEDialect(CodeWriter writer) {
			writer.append("switch on ");
			expression.ToDialect(writer);
			writer.append(":\n");
			writer.indent();
			foreach(SwitchCase sc in switchCases)
				sc.caseToEDialect(writer);
			if(defaultCase!=null) {
				writer.append("otherwise:\n");
				writer.indent();
				defaultCase.ToDialect(writer);
				writer.dedent();
			}
			writer.dedent();
		}

		override
		protected void toPDialect(CodeWriter writer) {
			toEDialect(writer);
		}
        override
        protected IType checkSwitchType(Context context)
        {
            return expression.check(context);
        }

        override
        public IValue interpret(Context context)
        {
			IValue switchValue = expression.interpret(context);
            return evaluateSwitch(context, switchValue, null);
        }
    }

}
