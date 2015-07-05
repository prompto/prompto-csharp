using System.Collections.Generic;
using prompto.declaration;
namespace prompto.grammar {

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
