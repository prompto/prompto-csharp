using presto.runtime;
using System;
using presto.parser;
using presto.expression;
using presto.grammar;
using presto.statement;
using presto.type;
using presto.utils;
using presto.value;

namespace presto.declaration
{

	public class SetterMethodDeclaration : ConcreteMethodDeclaration, IExpression
    {

        public SetterMethodDeclaration(String name, StatementList instructions)
            : base(name, null, null, instructions)
        {
        }

		protected override void toODialect(CodeWriter writer) {
			writer.append("setter ");
			writer.append(name);
			writer.append(" {\n");
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
			writer.append("}\n");
		}

		protected override void toEDialect(CodeWriter writer) {
			writer.append("define ");
			writer.append(name);
			writer.append(" setter doing:\n");
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
		}	

		protected override void toSDialect(CodeWriter writer) {
			writer.append("def ");
			writer.append(name);
			writer.append(" setter():\n");
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
		}	
        
		public override void check(ConcreteCategoryDeclaration category, Context context)
        {
            // TODO Auto-generated method stub

        }

		public override IType check(Context context)
        {
            // TODO Auto-generated method stub
            return null;
        }


    }

}