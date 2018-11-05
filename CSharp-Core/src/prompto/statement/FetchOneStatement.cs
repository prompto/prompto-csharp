using System;
using prompto.expression;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.parser;
using prompto.value;

namespace prompto.statement
{
	public class FetchOneStatement : FetchOneExpression, IStatement
	{
		string name;
		StatementList stmts;

		public FetchOneStatement(CategoryType type, IExpression predicate, string name, StatementList stmts) 
			: base(type, predicate)
		{
			this.name = name;
			this.stmts = stmts;
		}

		public bool CanReturn { get { return false; } }

		public bool IsSimple { get { return false; } }

		public override IType check(runtime.Context context)
		{
			base.check(context);
			context = context.newChildContext();
			context.registerValue(new Variable(name, type));
			stmts.check(context, null);
			return VoidType.Instance;
		}

		public override IValue interpret(Context context)
		{
			IValue record = base.interpret(context);
			context = context.newChildContext();
			context.registerValue(new Variable(name, type));
			context.setValue(name, record);
			stmts.interpret(context);
			return null;
		}

		public override void ToDialect(CodeWriter writer)
		{
			base.ToDialect(writer);
			writer.append(" then with ").append(name);
			if (writer.getDialect() == Dialect.O)
				writer.append(" {");
			else
				writer.append(":");
			writer = writer.newChildWriter();
			writer.getContext().registerValue(new Variable(name, type));
			writer.newLine().indent();
			stmts.ToDialect(writer);
			writer.dedent();
			if (writer.getDialect() == Dialect.O)
				writer.append("}");
		}
	}
}
