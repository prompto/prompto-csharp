using presto.runtime;
namespace presto.declaration
{

    public interface ICategoryMethodDeclaration : IMethodDeclaration
    {

        void check(ConcreteCategoryDeclaration declaration, Context context);

    }
}
