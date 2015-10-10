using prompto.type;
using prompto.runtime;
using prompto.value;
using prompto.utils;
using prompto.parser;

namespace prompto.statement
{

	public class WithSingletonStatement : BaseStatement
	{

		CategoryType type;
		StatementList statements;

		public WithSingletonStatement (CategoryType type, StatementList statements)
		{
			this.type = type;
			this.statements = statements;
		}

		public override IType check (Context context)
		{
			Context instanceContext = context.newInstanceContext (type);
			Context childContext = instanceContext.newChildContext ();
			return statements.check (childContext, null);
		}

		public override IValue interpret (Context context)
		{
			// TODO synchronize
			ConcreteInstance instance = context.loadSingleton (type);
			Context instanceContext = context.newInstanceContext (instance);
			Context childContext = instanceContext.newChildContext ();
			return statements.interpret (childContext);
		}

		public override void ToDialect (CodeWriter writer)
		{
			switch (writer.getDialect ()) {
			case Dialect.E:
				toEDialect (writer);
				break;
			case Dialect.O:
				toODialect (writer);
				break;
			case Dialect.S:
				toPDialect (writer);
				break;
			}
		}

		private void toEDialect (CodeWriter writer)
		{
			writer.append ("with ");
			type.ToDialect (writer);
			writer.append (", do:\n");
			writer.indent ();
			statements.ToDialect (writer);
			writer.dedent ();
		}

		private void toODialect (CodeWriter writer)
		{
			writer.append ("with (");
			type.ToDialect (writer);
			writer.append (")");
			bool oneLine = statements.Count == 1 && (statements[0] is SimpleStatement);
			if (!oneLine)
				writer.append (" {");
			writer.newLine ();
			writer.indent ();
			statements.ToDialect (writer);
			writer.dedent ();
			if (!oneLine) {
				writer.append ("}");
				writer.newLine ();
			}		
		}

		private void toPDialect (CodeWriter writer)
		{
			writer.append ("with ");
			type.ToDialect (writer);
			writer.append (":\n");
			writer.indent ();
			statements.ToDialect (writer);
			writer.dedent ();
		}

	}
}