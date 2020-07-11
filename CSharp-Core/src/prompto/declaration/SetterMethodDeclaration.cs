using prompto.runtime;
using System;
using prompto.expression;
using prompto.statement;
using prompto.type;
using prompto.utils;
using prompto.error;

namespace prompto.declaration
{

	public class SetterMethodDeclaration : ConcreteMethodDeclaration, IExpression
    {

        public SetterMethodDeclaration(String name, StatementList instructions)
            : base(name, null, null, instructions)
        {
        }

		protected override void ToODialect(CodeWriter writer) {
			writer.append("setter ");
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
			writer.append(" as setter doing:\n");
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
		}	

		protected override void ToMDialect(CodeWriter writer) {
			writer.append("def ");
			writer.append(name);
			writer.append(" setter():\n");
			writer.indent();
			statements.ToDialect(writer);
			writer.dedent();
		}	
        
	
		public override IType check(Context context)
        {
			AttributeDeclaration decl = context.getRegisteredDeclaration<AttributeDeclaration>(name);
			return decl.getIType();
        }

		public AttributeDeclaration CheckAttribute(Context context)
		{
			throw new SyntaxError("Expected an attribute, got: " + this.ToString());
		}

	}

}