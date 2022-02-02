
namespace prompto.declaration
{
    public class MethodDeclarationReference : MethodDeclarationWrapper
    {
       
        public MethodDeclarationReference(IMethodDeclaration wrapped)
            : base(wrapped)
        { 
        }

        public bool IsReference()
        {
            return true;
        }
    }
}
