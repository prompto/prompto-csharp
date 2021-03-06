using System;
using prompto.runtime;
using prompto.error;
using prompto.type;
using prompto.grammar;
using prompto.expression;
using prompto.literal;
using prompto.utils;
using prompto.value;
using prompto.param;


namespace prompto.statement
{

    public class SwitchErrorStatement : BaseSwitchStatement
    {

        String errorName;
        StatementList statements;
        StatementList alwaysStatements;

        public SwitchErrorStatement(String errorName, StatementList statements)
        {
            this.errorName = errorName;
            this.statements = statements;
        }

        public SwitchErrorStatement(String errorName, StatementList statements, SwitchCaseList handlers, StatementList anyStmts, StatementList finalStmts)
            : base(handlers, anyStmts)
        {
            this.errorName = errorName;
            this.statements = statements;
            this.alwaysStatements = finalStmts;
        }

        public void setAlwaysInstructions(StatementList list)
        {
            alwaysStatements = list;
        }

        public override void ToDialect(CodeWriter writer)
        {
            writer = writer.newLocalWriter();
            writer.getContext().registerValue(new ErrorVariable(errorName));
            base.ToDialect(writer);
        }

        protected override void ToODialect(CodeWriter writer)
        {
            writer.append("try (");
            writer.append(errorName);
            writer.append(") {\n");
            writer.indent();
            statements.ToDialect(writer);
            writer.dedent();
            writer.append("} ");
            foreach (SwitchCase sc in switchCases)
                sc.catchToODialect(writer);
            if (defaultCase != null)
            {
                writer.append("catch(any) {\n");
                writer.indent();
                defaultCase.ToDialect(writer);
                writer.dedent();
                writer.append("}");
            }
            if (alwaysStatements != null)
            {
                writer.append("finally {\n");
                writer.indent();
                alwaysStatements.ToDialect(writer);
                writer.dedent();
                writer.append("}");
            }
            writer.newLine();
        }

        
        protected override void toMDialect(CodeWriter writer)
        {
            writer.append("try ");
            writer.append(errorName);
            writer.append(":\n");
            writer.indent();
            statements.ToDialect(writer);
            writer.dedent();
            foreach (SwitchCase sc in switchCases)
                sc.catchToMDialect(writer);
            if (defaultCase != null)
            {
                writer.append("except:\n");
                writer.indent();
                defaultCase.ToDialect(writer);
                writer.dedent();
            }
            if (alwaysStatements != null)
            {
                writer.append("finally:\n");
                writer.indent();
                alwaysStatements.ToDialect(writer);
                writer.dedent();
            }
            writer.newLine();
        }

        
        protected override void ToEDialect(CodeWriter writer)
        {
            writer.append("switch on ");
            writer.append(errorName);
            writer.append(" doing:\n");
            writer.indent();
            statements.ToDialect(writer);
            writer.dedent();
            foreach (SwitchCase sc in switchCases)
                sc.catchToEDialect(writer);
            if (defaultCase != null)
            {
                writer.append("when any:\n");
                writer.indent();
                defaultCase.ToDialect(writer);
                writer.dedent();
            }
            if (alwaysStatements != null)
            {
                writer.append("always:\n");
                writer.indent();
                alwaysStatements.ToDialect(writer);
                writer.dedent();
            }
        }
        
        protected override void checkSwitchCasesType(Context context)
        {
            Context local = context.newLocalContext();
            local.registerValue(new ErrorVariable(errorName));
            base.checkSwitchCasesType(local);
        }

        
        protected override IType checkSwitchType(Context context)
        {
            return new EnumeratedCategoryType("Error");
        }

        
        protected override void collectReturnTypes(Context context, TypeMap types)
        {
            IType type = statements.check(context, null);
            if (type != VoidType.Instance)
                types.add(type);
            Context local = context.newLocalContext();
            local.registerValue(new ErrorVariable(errorName));
            base.collectReturnTypes(local, types);
            if (alwaysStatements != null)
            {
                type = alwaysStatements.check(context, null);
                if (type != VoidType.Instance)
                    types.add(type);
            }
        }

        
        public override IValue interpret(Context context)
        {
            IValue result = null;
            try
            {
                result = statements.interpret(context);
            }
            catch (ExecutionError e)
            {
                IValue switchValue = populateError(e, context);
                result = evaluateSwitch(context, switchValue, e);
            }
            finally
            {
                if (alwaysStatements != null)
                    alwaysStatements.interpret(context);
            }
            return result;
        }

        private IValue populateError(ExecutionError e, Context context)
        {
            IExpression exp = e.getExpression(context);
            if (exp == null)
            {
                ArgumentList args = new ArgumentList();
                args.Add(new Argument(new UnresolvedParameter("name"), new TextLiteral(e.GetType().Name)));
                args.Add(new Argument(new UnresolvedParameter("text"), new TextLiteral(e.Message)));
                ConstructorExpression ctor = new ConstructorExpression(new CategoryType("Error"), null, args);
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
