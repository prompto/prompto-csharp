
using System.Collections.Generic;
using presto.runtime;
using System;
using presto.error;
using presto.type;
using presto.grammar;
using presto.utils;
using presto.parser;
using presto.value;


namespace presto.statement
{

    public abstract class BaseSwitchStatement : BaseStatement
    {

        protected SwitchCaseList switchCases;
		protected StatementList defaultCase;

        public BaseSwitchStatement()
        {
            this.switchCases = new SwitchCaseList();
            this.defaultCase = null;
        }

        public BaseSwitchStatement(SwitchCaseList switchCases, StatementList defaultCase)
        {
            this.switchCases = switchCases != null ? switchCases : new SwitchCaseList();
            this.defaultCase = defaultCase;
        }

        public void addSwitchCase(SwitchCase switchCase)
        {
            switchCases.Add(switchCase);
        }

        public void setDefaultCase(StatementList defaultCase)
        {
            this.defaultCase = defaultCase;
        }

        override
        public IType check(Context context)
        {
            checkSwitchCasesType(context);
            return checkReturnType(context);
        }

        protected virtual void checkSwitchCasesType(Context context)
        {
            IType type = checkSwitchType(context);
            foreach (SwitchCase sc in switchCases)
                sc.checkSwitchType(context, type);
        }

        abstract protected IType checkSwitchType(Context context);

        private IType checkReturnType(Context context)
        {
            TypeMap types = new TypeMap();
            collectReturnTypes(context, types);
            return types.inferType(context);
        }

        protected virtual void collectReturnTypes(Context context, TypeMap types)
        {
            foreach (SwitchCase sc in switchCases)
            {
                IType type = sc.checkReturnType(context);
                if (type != VoidType.Instance)
					types[type.GetName()] = type;
            }
            if (defaultCase != null)
            {
                IType type = defaultCase.check(context);
                if (type != VoidType.Instance)
					types[type.GetName()] = type;
            }
        }

		protected IValue evaluateSwitch(Context context, Object switchValue, ExecutionError toThrow)
        {
            foreach (SwitchCase sc in switchCases)
            {
                if (sc.matches(context, switchValue))
                    return sc.interpret(context);
            }
            if (defaultCase != null)
                return defaultCase.interpret(context);
            if (toThrow != null)
                throw toThrow;
            return null;
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
			case Dialect.P:
				toPDialect(writer);
				break;
			}
		}

		protected abstract void toEDialect(CodeWriter writer);
		protected abstract void toODialect(CodeWriter writer);
		protected abstract void toPDialect(CodeWriter writer);
    }

}
