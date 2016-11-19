using prompto.runtime;
using System;
using prompto.statement;
using prompto.expression;
using prompto.grammar;
using prompto.type;
using prompto.utils;
using prompto.parser;
using prompto.value;


namespace prompto.declaration
{

	public class GetterMethodDeclaration : ConcreteMethodDeclaration, IExpression
    {

		public GetterMethodDeclaration(String name, StatementList statements)
			: base(name, null, null, statements)
        {
        }

        override
        public void check(ConcreteCategoryDeclaration category, Context context)
        {
            // TODO Auto-generated method stub

        }

        override
        public IType check(Context context)
        {
            // TODO Auto-generated method stub
            return null;
        }

		protected override void ToODialect(CodeWriter writer) {
			writer.append("getter ");
			writer.append(name);
			writer.append(" {\n");
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
			writer.append("}\n");
		}

		protected override void ToEDialect(CodeWriter writer) {
			writer.append("define ");
			writer.append(name);
			writer.append(" as getter doing:\n");
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
		}

		protected override void ToMDialect(CodeWriter writer) {
			writer.append("def ");
			writer.append(name);
			writer.append(" getter():\n");
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
		}
    }
}
