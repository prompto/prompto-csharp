using System;
using prompto.parser;
using prompto.runtime;
using prompto.statement;
using prompto.type;
using prompto.utils;
using prompto.value;

namespace prompto.grammar
{
    public class ThenWith
    {

        public static ThenWith OrEmpty(ThenWith then)
        {
            return then != null ? then : new ThenWith(null, null);
        }

        public ThenWith(String name, StatementList statements)
        {
            this.Name = name;
            this.Statements = statements;

        }

        public String Name { get; set; }

        public StatementList Statements { get; set; }

        public IType check(Context context, IType type)
        {
            context = context.newChildContext();
            context.registerValue(new Variable(Name, type));
            Statements.check(context, null);
            return VoidType.Instance;

        }

        public IValue interpret(Context context, IValue value)
        {
            context = context.newChildContext();
            context.registerValue(new Variable(Name, value.GetIType()));
            context.setValue(Name, value);
            Statements.interpret(context);
            return null;
        }

        public void ToDialect(CodeWriter writer, IType type)
        {
            writer.append(" then with ").append(Name);
            if (writer.getDialect() == Dialect.O)
                writer.append(" {");
            else
                writer.append(":");
            writer = writer.newChildWriter();
            writer.getContext().registerValue(new Variable(Name, type));
            writer.newLine().indent();
            Statements.ToDialect(writer);
            writer.dedent();
            if (writer.getDialect() == Dialect.O)
                writer.append("}").newLine();
        }
    }
}
