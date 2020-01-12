using prompto.statement;
using prompto.utils;
using prompto.expression;
using System;
using prompto.type;
using prompto.runtime;
using prompto.error;
using prompto.value;
using prompto.parser;

namespace prompto.declaration
{

    public class TestMethodDeclaration : BaseDeclaration
    {

        StatementList statements;
        ExpressionList assertions;
        SymbolExpression error;

        public TestMethodDeclaration(String name, StatementList stmts, ExpressionList exps, SymbolExpression error)
            : base(name)
        {
            this.statements = stmts;
            this.assertions = exps;
            this.error = error;
        }

        public override IType check(Context context)
        {
            // TODO
            return VoidType.Instance;
        }

        public override void register(Context context)
        {
            context.registerDeclaration(this);
        }

        public override IType GetIType(Context context)
        {
            return VoidType.Instance;
        }

        public void interpret(Context context)
        {
            if (interpretBody(context))
            {
                interpretError(context);
                interpretAsserts(context);
            }
        }

        private void interpretError(Context context)
        {
            // we land here only if no error was raised
            if (error != null)
                printMissingError(context, error.getName(), "no error");
        }

        private void interpretAsserts(Context context)
        {
            if (assertions == null)
                return;
            context.enterMethod(this);
            try
            {
                bool success = true;
                foreach (IExpression exp in assertions)
                {
                    success &= ((IAssertion)exp).interpretAssert(context, this);
                }
                if (success)
                    printSuccess(context);
            }
            finally
            {
                context.leaveMethod(this);
            }
        }

        public void printMissingError(Context context, String expected, String actual)
        {
            Console.Write(this.name + " test failed while expecting: " + expected + ", found: " + actual);
        }

        public void printAssertionFailed(Context context, String expected, String actual)
        {
            Console.Write(this.name + " test failed while verifying: " + expected + ", found: " + actual);
        }

        private void printSuccess(Context context)
        {
            Console.Write(this.name + " test successful");
        }

        private bool interpretBody(Context context)
        {
            context.enterMethod(this);
            try
            {
                statements.interpret(context);
                return true;
            }
            catch (ExecutionError e)
            {
                interpretError(context, e);
                // no more to execute
                return false;
            }
            finally
            {
                context.leaveMethod(this);
            }
        }

        private void interpretError(Context context, ExecutionError e)
        {
            IValue actual = e.interpret(context, "__test_error__");
            IValue expectedError = error == null ? null : error.interpret(context);
            if (expectedError != null && expectedError.Equals(actual))
                printSuccess(context);
            else
            {
                String actualName = ((IInstance)actual).GetMemberValue(context, "name", false).ToString();
                String expectedName = error == null ? "SUCCESS" : error.getName();
                printMissingError(context, expectedName, actualName);
            }
        }

        public override void ToDialect(CodeWriter writer)
        {
            if (writer.isGlobalContext())
                writer = writer.newLocalWriter();
            switch (writer.getDialect())
            {
                case Dialect.E:
                    ToEDialect(writer);
                    break;
                case Dialect.O:
                    ToODialect(writer);
                    break;
                case Dialect.M:
                    toPDialect(writer);
                    break;
            }
        }

        protected void toPDialect(CodeWriter writer)
        {
            writer.append("def test ");
            writer.append(name);
            writer.append(" ():\n");
            writer.indent();
            statements.ToDialect(writer);
            writer.dedent();
            writer.append("verifying:");
            if (error != null)
            {
                writer.append(" ");
                error.ToDialect(writer);
                writer.append("\n");
            }
            else
            {
                writer.append("\n");
                writer.indent();
                foreach (IExpression exp in assertions)
                {
                    exp.ToDialect(writer);
                    writer.append("\n");
                }
                writer.dedent();
            }
        }

        protected void ToEDialect(CodeWriter writer)
        {
            writer.append("define ");
            writer.append(name);
            writer.append(" as test method doing:\n");
            writer.indent();
            statements.ToDialect(writer);
            writer.dedent();
            writer.append("and verifying");
            if (error != null)
            {
                writer.append(" ");
                error.ToDialect(writer);
                writer.append("\n");
            }
            else
            {
                writer.append(":\n");
                writer.indent();
                foreach (IExpression exp in assertions)
                {
                    exp.ToDialect(writer);
                    writer.append("\n");
                }
                writer.dedent();
            }
        }

        protected void ToODialect(CodeWriter writer)
        {
            writer.append("test method ");
            writer.append(name);
            writer.append(" () {\n");
            writer.indent();
            statements.ToDialect(writer);
            writer.dedent();
            writer.append("} verifying ");
            if (error != null)
            {
                error.ToDialect(writer);
                writer.append(";\n");
            }
            else
            {
                writer.append("{\n");
                writer.indent();
                foreach (IExpression exp in assertions)
                {
                    exp.ToDialect(writer);
                    writer.append(";\n");
                }
                writer.dedent();
                writer.append("}\n");
            }
        }
    }
}
