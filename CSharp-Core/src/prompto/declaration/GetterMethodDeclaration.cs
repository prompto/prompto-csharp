using prompto.runtime;
using System;
using prompto.statement;
using prompto.expression;
using prompto.type;
using prompto.utils;
using prompto.error;
using prompto.value;

namespace prompto.declaration
{

    public class GetterMethodDeclaration : ConcreteMethodDeclaration, IExpression
    {

        public GetterMethodDeclaration(String name, StatementList statements)
            : base(name, null, null, statements)
        {
        }


        public override IType check(Context context)
        {
            AttributeDeclaration decl = context.getRegisteredDeclaration<AttributeDeclaration>(name);
            return decl.getIType();
        }

        public virtual IType checkReference(Context context)
        {
            return check(context);
        }

        public virtual IValue interpretReference(Context context)
        {
            return interpret(context);
        }


        public AttributeDeclaration CheckAttribute(Context context)
        {
            throw new SyntaxError("Expected an attribute, got: " + this.ToString());
        }

        protected override void ToODialect(CodeWriter writer)
        {
            writer.append("getter ");
            writer.append(name);
            writer.append(" {\n");
            writer.indent();
            statements.ToDialect(writer);
            writer.dedent();
            writer.append("}\n");
        }

        protected override void ToEDialect(CodeWriter writer)
        {
            writer.append("define ");
            writer.append(name);
            writer.append(" as getter doing:\n");
            writer.indent();
            statements.ToDialect(writer);
            writer.dedent();
        }

        protected override void ToMDialect(CodeWriter writer)
        {
            writer.append("def ");
            writer.append(name);
            writer.append(" getter():\n");
            writer.indent();
            statements.ToDialect(writer);
            writer.dedent();
        }
    }
}
