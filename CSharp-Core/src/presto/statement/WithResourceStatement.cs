using presto.runtime;
using System;
using presto.parser;
using presto.type;
using presto.value;
using presto.utils;


namespace presto.statement
{

    public class WithResourceStatement : BaseStatement
    {

        AssignVariableStatement resource;
        StatementList instructions;

        public WithResourceStatement(AssignVariableStatement resource, StatementList instructions)
        {
            this.resource = resource;
            this.instructions = instructions;
        }

        override
        public void ToDialect(CodeWriter writer)
        {
			switch(writer.getDialect()) {
			case Dialect.E:
				toEDialect(writer);
				break;
			case Dialect.O:
				toODialect(writer);
				break;
			case Dialect.S:
				toPDialect(writer);
				break;
			}
		}

		private void toEDialect(CodeWriter writer) {
			writer.append("with ");
			resource.ToDialect(writer);
			writer.append(", do:\n");
			writer.indent();
			instructions.ToDialect(writer);
			writer.dedent();
		}

		private void toODialect(CodeWriter writer) {
			writer.append("with (");
			resource.ToDialect(writer);
			writer.append(")");
			bool oneLine = instructions.Count==1 && (instructions[0] is SimpleStatement);
			if(!oneLine)
				writer.append(" {");
			writer.newLine();
			writer.indent();
			instructions.ToDialect(writer);
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
			instructions.ToDialect(writer);
			writer.dedent();
		}

        override
        public IType check(Context context)
        {
            context = context.newResourceContext();
            resource.checkResource(context);
            return instructions.check(context);
        }

        override
		public IValue interpret(Context context)
        {
            context = context.newResourceContext();
            try
            {
                resource.interpret(context);
                return instructions.interpret(context);
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
