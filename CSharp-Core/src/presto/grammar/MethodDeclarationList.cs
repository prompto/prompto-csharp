using System.Collections.Generic;
using presto.declaration;
namespace presto.grammar {

    public class MethodDeclarationList : List<IMethodDeclaration>
    {

        public MethodDeclarationList()
        {
        }

		public MethodDeclarationList(IMethodDeclaration method)
        {
            this.Add(method);
        }

        /* for unified grammar */
		public void add(IMethodDeclaration method)
        {
            this.Add(method);
        }
    }
		
}
