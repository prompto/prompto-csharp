using prompto.runtime;
using System;
using prompto.parser;
using prompto.type;
using prompto.value;
using prompto.utils;


namespace prompto.statement
{

    public class WithResourceStatement : BaseStatement
    {

        AssignVariableStatement resource;
		StatementList statements;

		public WithResourceStatement(AssignVariableStatement resource, StatementList statements)
        {
            this.resource = resource;
			this.statements = statements;
        }

        override
        public void ToDialect(CodeWriter writer)
        {
			switch(writer.getDialect()) {
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

		private void ToEDialect(CodeWriter writer) {
			writer.append("with ");
			resource.ToDialect(writer);
			writer.append(", do:\n");
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
		}

		private void ToODialect(CodeWriter writer) {
			writer.append("with (");
			resource.ToDialect(writer);
			writer.append(")");
			bool oneLine = statements.Count==1 && (statements[0] is SimpleStatement);
			if(!oneLine)
				writer.append(" {");
			writer.newLine();
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
			if(!oneLine) {
				writer.append("}");
				writer.newLine();
			}		
		}

		private void toPDialect(CodeWriter writer) {
			writer.append("with ");
			resource.ToDialect(writer);
			writer.append(":\n");
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
		}

        override
        public IType check(Context context)
        {
            context = context.newResourceContext();
            resource.checkResource(context);
            return statements.check(context, null);
        }

        override
		public IValue interpret(Context context)
        {
            context = context.newResourceContext();
            try
            {
                resource.interpret(context);
                return statements.interpret(context);
            }
            finally
            {
				IValue res = context.getValue(resource.getName());
                if (res is IResource)
                    ((IResource)res).close();
            }
        }

    }

}
