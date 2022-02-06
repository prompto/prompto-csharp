
namespace prompto.declaration
{
    public class MethodDeclarationReference : MethodDeclarationWrapper
    {
       
        public MethodDeclarationReference(IMethodDeclaration wrapped)
            : base(wrapped)
        { 
        }

        public override bool IsReference()
        {
            return true;
        }
    }
}
