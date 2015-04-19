using System;
using presto.runtime;
using presto.error;
using presto.type;
using presto.grammar;
using presto.expression;
using presto.literal;
using presto.utils;
using presto.value;


namespace presto.statement
{

    public class SwitchErrorStatement : BaseSwitchStatement
    {

        String errorName;
        StatementList instructions;
        StatementList alwaysInstructions;

        public SwitchErrorStatement(String errorName, StatementList instructions)
        {
            this.errorName = errorName;
            this.instructions = instructions;
        }

        public SwitchErrorStatement(String errorName, StatementList instructions, SwitchCaseList handlers, StatementList anyStmts, StatementList finalStmts)
            : base(handlers, anyStmts)
        {
            this.errorName = errorName;
            this.instructions = instructions;
            this.alwaysInstructions = finalStmts;
        }

        public void setAlwaysInstructions(StatementList list)
        {
            alwaysInstructions = list;
        }

		override
		protected void toODialect(CodeWriter writer) {
			writer.append("try (");
			writer.append(errorName);
			writer.append(") {\n");
			writer.indent();
			instructions.ToDialect(writer);
			writer.dedent();
			writer.append("} ");
			foreach(SwitchCase sc in switchCases)
				sc.catchToODialect(writer);
			if(defaultCase!=null) {
				writer.append("catch(any) {\n");
				writer.indent();
				defaultCase.ToDialect(writer);
				writer.dedent();
				writer.append("}");
			}
			if(alwaysInstructions!=null) {
				writer.append("finally {\n");
				writer.indent();
				alwaysInstructions.ToDialect(writer);
				writer.dedent();
				writer.append("}");
			}
			writer.newLine();
		}

		override
		protected void toPDialect(CodeWriter writer) {
			writer.append("try ");
			writer.append(errorName);
			writer.append(":\n");
			writer.indent();
			instructions.ToDialect(writer);
			writer.dedent();
			foreach(SwitchCase sc in switchCases)
				sc.catchToPDialect(writer);
			if(defaultCase!=null) {
				writer.append("except:\n");
				writer.indent();
				defaultCase.ToDialect(writer);
				writer.dedent();
			}
			if(alwaysInstructions!=null) {
				writer.append("finally:\n");
				writer.indent();
				alwaysInstructions.ToDialect(writer);
				writer.dedent();
			}
			writer.newLine();
		}

		override
		protected void toEDialect(CodeWriter writer) {
			writer.append("switch on ");
			writer.append(errorName);
			writer.append(" doing:\n");
			writer.indent();
			instructions.ToDialect(writer);
			writer.dedent();
			foreach(SwitchCase sc in switchCases)
				sc.catchToEDialect(writer);
			if(defaultCase!=null) {
				writer.append("when any:\n");
				writer.indent();
				defaultCase.ToDialect(writer);
				writer.dedent();
			}
			if(alwaysInstructions!=null) {
				writer.append("always:\n");
				writer.indent();
				alwaysInstructions.ToDialect(writer);
				writer.dedent();
			}
		}
        override
        protected void checkSwitchCasesType(Context context)
        {
            Context local = context.newLocalContext();
            local.registerValue(new ErrorVariable(errorName));
            base.checkSwitchCasesType(local);
        }

        override
        protected IType checkSwitchType(Context context)
        {
            return new EnumeratedCategoryType("Error");
        }

        override
        protected void collectReturnTypes(Context context, TypeMap types)
        {
            IType type = instructions.check(context);
            if (type != VoidType.Instance)
				types[type.GetName()] = type;
            Context local = context.newLocalContext();
            local.registerValue(new ErrorVariable(errorName));
            base.collectReturnTypes(local, types);
            if (alwaysInstructions != null)
            {
                type = alwaysInstructions.check(context);
                if (type != VoidType.Instance)
					types[type.GetName()] = type;
            }
        }

        override
		public IValue interpret(Context context)
        {
			IValue result = null;
            try
            {
                result = instructions.interpret(context);
            }
            catch (ExecutionError e)
            {
				IValue switchValue = populateError(e, context);
                result = evaluateSwitch(context, switchValue, e);
            }
            finally
            {
                if (alwaysInstructions != null)
                    alwaysInstructions.interpret(context);
            }
            return result;
        }

		private IValue populateError(ExecutionError e, Context context)
        {
            IExpression exp = e.getExpression(context);
			if (exp == null)
            {
                ConstructorExpression ctor = new ConstructorExpression(new CategoryType("Error"), false, null);
                ArgumentAssignmentList args = new ArgumentAssignmentList();
                args.Add(new ArgumentAssignment(new UnresolvedArgument("name"), new TextLiteral(e.GetType().Name)));
                args.Add(new ArgumentAssignment(new UnresolvedArgument("text"), new TextLiteral(e.Message)));
                ctor.setAssignments(args);
				exp = ctor;
            }
            if (context.getRegisteredValue<INamed>(errorName) == null)
                context.registerValue(new ErrorVariable(errorName));
            IValue value = exp.interpret(context);
			context.setValue(errorName, value);
			return value;
        }

    }
}
