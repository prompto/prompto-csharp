using presto.runtime;
using System;
using presto.statement;
using presto.expression;
using presto.grammar;
using presto.type;
using presto.utils;
using presto.parser;
using presto.value;


namespace presto.declaration
{

    public class GetterMethodDeclaration : BaseCategoryMethodDeclaration, IExpression
    {

        public GetterMethodDeclaration(String name, StatementList instructions)
            : base(name, null, instructions)
        {
        }

        override
        public int GetHashCode()
        {
            return getName().GetHashCode();
        }

        override
        public void check(ConcreteCategoryDeclaration category, Context context)
        {
            // TODO Auto-generated method stub

        }

		override
		public IType getReturnType() {
			// TODO Auto-generated method stub
			return null;
		}


        override
        public IType GetType(Context context)
        {
            // TODO Auto-generated method stub
            return null;
        }

        override
		public IValue interpret(Context context)
        {
            return instructions.interpret(context);
        }

        override
        public IType check(Context context)
        {
            // TODO Auto-generated method stub
            return null;
        }

		override
		public void ToDialect(CodeWriter writer) {
			switch(writer.getDialect()) {
			case Dialect.E:
				toEDialect(writer);
				break;
			case Dialect.O:
				toODialect(writer);
				break;
			case Dialect.P:
				toPDialect(writer);
				break;
			}
		}

		private void toODialect(CodeWriter writer) {
			writer.append("getter ");
			writer.append(name);
			writer.append(" {\n");
			writer.indent();
			instructions.ToDialect(writer);
			writer.dedent();
			writer.append("}\n");
		}

		private void toEDialect(CodeWriter writer) {
			writer.append("define ");
			writer.append(name);
			writer.append(" getter doing:\n");
			writer.indent();
			instructions.ToDialect(writer);
			writer.dedent();
		}

		private void toPDialect(CodeWriter writer) {
			writer.append("def ");
			writer.append(name);
			writer.append(" getter():\n");
			writer.indent();
			instructions.ToDialect(writer);
			writer.dedent();
		}
    }
}
