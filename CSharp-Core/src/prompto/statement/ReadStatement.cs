


using System;
using prompto.expression;
using prompto.runtime;
using prompto.type;
using prompto.utils;
using prompto.value;
using prompto.parser;

namespace prompto.statement
{
	public class ReadStatement : ReadAllExpression, IStatement
	{

		String name;
		StatementList stmts;


		public ReadStatement(IExpression resource, String name, StatementList stmts)
			: base(resource)
		{
			this.name = name;
			this.stmts = stmts;
		}

		public bool CanReturn
        {
			get {
				return false;
			}
        }
	
        public bool IsSimple
        {
			get
			{
				return false;
			}
		}

        public override IType check(Context context)
		{
			base.check(context);
			context = context.newChildContext();
			context.registerValue(new Variable(name, TextType.Instance));
			stmts.check(context, VoidType.Instance);
			return VoidType.Instance;
		}

		public override IValue interpret(Context context)
		{
			IValue result = base.interpret(context);
			context = context.newChildContext();
			context.registerValue(new Variable(name, TextType.Instance));
			context.setValue(name, result);
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
			writer.getContext().registerValue(new Variable(name, TextType.Instance));
			writer.newLine().indent();
			stmts.ToDialect(writer);
			writer.dedent();
			if (writer.getDialect() == Dialect.O)
				writer.append("}");
		}
	}
}
