using System.Collections.Generic;
using presto.declaration;
namespace presto.grammar {

    public class CategoryMethodDeclarationList : List<ICategoryMethodDeclaration>
    {

        public CategoryMethodDeclarationList()
        {
        }

        public CategoryMethodDeclarationList(ICategoryMethodDeclaration method)
        {
            this.Add(method);
        }

        /* for unified grammar */
        public void add(ICategoryMethodDeclaration method)
        {
            this.Add(method);
        }
    }
		
}
