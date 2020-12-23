using System;
using System.Collections.Generic;
using prompto.error;
using prompto.grammar;
using prompto.parser;
using prompto.runtime;
using prompto.statement;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.expression
{
    public class ArrowExpression : PredicateExpression, IExpression
    {

        public ArrowExpression(IdentifierList args, String argsSuite, String arrowSuite)
        {
            this.Arguments = args;
            this.ArgsSuite = argsSuite;
            this.ArrowSuite = arrowSuite;
        }

        public IdentifierList Arguments { get; set; }
        public String ArgsSuite { get; set; }
        public String ArrowSuite { get; set; }
        public StatementList Statements { get; set; }

        public override ArrowExpression ToArrowExpression()
        {
            return this;
        }

        public override IType check(Context context)
        {
            return CheckReturnType(context, null);
        }

        public IType CheckReturnType(Context context, IType returnType)
        {
            return Statements.check(context, returnType);
        }

        public override IType CheckFilter(Context context, IType itemType)
        {
            if (Arguments == null || Arguments.Count != 1)
                throw new SyntaxError("Expecting 1 parameter only!");
            context = context.newChildContext();
            context.registerValue(new Variable(Arguments[0], itemType));
            return Statements.check(context, null);
        }

        public override IValue interpret(Context context)
        {
            return Statements.interpret(context);
        }

        public override string ToString()
        {
            return ToString(Context.newGlobalsContext());
        }

        public String ToString(Context context)
        {
            try
            {
                CodeWriter writer = new CodeWriter(Dialect.E, context);
                ToDialect(writer);
                return writer.ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public override void ToDialect(CodeWriter writer)
        {
            ArgsToDialect(writer);
            if (ArgsSuite != null)
                writer.append(ArgsSuite);
            writer.append("=>");
            if (ArrowSuite != null)
                writer.append(ArrowSuite);
            BodyToDialect(writer);
        }

        private void ArgsToDialect(CodeWriter writer)
        {
            if (Arguments == null || Arguments.Count == 0)
                writer.append("()");
            else if (Arguments.Count == 1)
                writer.append(Arguments[0]);
            else
            {
                writer.append("(");
                Arguments.ToDialect(writer, false);
                writer.append(")");
            }

        }

        private void BodyToDialect(CodeWriter writer)
        {
            if (Statements.Count == 1 && Statements[0] is ReturnStatement)
                ((ReturnStatement)Statements[0]).getExpression().ToDialect(writer);
            else
            {
                writer.append("{").newLine().indent();
                Statements.ToDialect(writer);
                writer.newLine().dedent().append("}").newLine();
            }
        }

        public override void FilteredToDialect(CodeWriter writer, IExpression source)
        {
            if (Arguments.Count != 1)
                throw new SyntaxError("Expecting 1 parameter only!");
            IType sourceType = source.check(writer.getContext());
            IType itemType = ((IterableType)sourceType).GetItemType();
            writer = writer.newChildWriter();
            writer.getContext().registerValue(new Variable(Arguments[0], itemType));
            switch (writer.getDialect())
            {
                case Dialect.E:
                case Dialect.M:
                    source.ToDialect(writer);
                    writer.append(" filtered where ");
                    this.ToDialect(writer);
                    break;
                case Dialect.O:
                    writer.append("filtered (");
                    source.ToDialect(writer);
                    writer.append(") where (");
                    this.ToDialect(writer);
                    writer.append(")");
                    break;
            }
        }

        public override void ContainsToDialect(CodeWriter writer)
        {
            writer.append("where ");
            if (writer.getDialect() == Dialect.O)
                writer.append("( ");
            this.ToDialect(writer);
            if (writer.getDialect() == Dialect.O)
                writer.append(" ) ");
        }

        public IExpression Expression
        {
            set
            {
                IStatement stmt = new ReturnStatement(value);
                Statements = new StatementList(stmt);
            }
        }

        public Predicate<IValue> GetFilter(Context context, IType itemType)
        {
            context = RegisterArrowArgs(context.newLocalContext(), itemType);
            return delegate (IValue o)
            {
                context.setValue(Arguments[0], o);
                IValue result = Statements.interpret(context);
                if (result is value.BooleanValue)
                    return ((value.BooleanValue)result).Value;
                else
                    throw new SyntaxError("Expecting a Boolean result!");
            };
        }

        public Comparer<IValue> GetComparer(Context context, IType itemType, bool descending)
        {
            context = RegisterArrowArgs(context.newLocalContext(), itemType);
            switch (Arguments.Count)
            {
                case 1:
                    return new ArrowComparer1Arg(context, descending, this);
                case 2:
                    return new ArrowComparer2Args(context, descending, this);
                default:
                    throw new SyntaxError("Expecting 1 or 2 parameters only!");
            }
        }

        protected Context RegisterArrowArgs(Context context, IType itemType)
        {
            foreach (String arg in Arguments)
                context.registerValue(new Variable(arg, itemType));
            return context;
        }

        internal abstract class ArrowComparer : Comparer<IValue>
        {
            protected Context context;
            protected bool descending;
            protected ArrowExpression arrow;

            internal ArrowComparer(Context context, bool descending, ArrowExpression arrow)
            {
                this.context = context;
                this.descending = descending;
                this.arrow = arrow;
            }

        }

        internal class ArrowComparer1Arg : ArrowComparer
        {
            internal ArrowComparer1Arg(Context context, bool descending, ArrowExpression arrow)
                : base(context, descending, arrow)
            {

            }

            public override int Compare(IValue o1, IValue o2)
            {
                context.setValue(arrow.Arguments[0], o1);
                IValue key1 = arrow.Statements.interpret(context);
                context.setValue(arrow.Arguments[0], o2);
                IValue key2 = arrow.Statements.interpret(context);
                int result = key1.CompareTo(context, key2);
                return descending ? -result : result;
            }
        }

        internal class ArrowComparer2Args : ArrowComparer
        {
            internal ArrowComparer2Args(Context context, bool descending, ArrowExpression arrow)
                : base(context, descending, arrow)
            {
            }

            public override int Compare(IValue o1, IValue o2)
            {
                context.setValue(arrow.Arguments[0], o1);
                context.setValue(arrow.Arguments[1], o2);
                IValue value = arrow.Statements.interpret(context);
                if (!(value is IntegerValue))
                    throw new SyntaxError("Expecting an Integer as result of key body!");
                long result = ((IntegerValue)value).LongValue;
                return (int)(descending ? -result : result);
            }
        }
    }

}
