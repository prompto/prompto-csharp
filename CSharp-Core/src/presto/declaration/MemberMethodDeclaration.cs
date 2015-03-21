using presto.runtime;
using presto.expression;
using presto.grammar;
using presto.statement;
using System;
using presto.type;
namespace presto.declaration
{

    public class MemberMethodDeclaration : ConcreteMethodDeclaration, IExpression, ICategoryMethodDeclaration
    {

        public MemberMethodDeclaration(String name, ArgumentList arguments, IType returnType, StatementList instructions)
            : base(name, arguments, returnType, instructions)
        {
        }

        public void check(ConcreteCategoryDeclaration declaration, Context context)
        {
            // TODO Auto-generated method stub

        }
    }
}