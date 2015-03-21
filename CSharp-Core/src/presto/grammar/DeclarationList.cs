using presto.runtime;
using System.Collections.Generic;
using presto.declaration;
using presto.utils;

namespace presto.grammar
{

	public class DeclarationList : List<IDeclaration>, IDialectElement
    {

        public DeclarationList()
        {
        }

        public DeclarationList(IDeclaration item)
        {
            Add(item);
        }


        public void register(Context context)
        {
            foreach (IDeclaration declaration in this)
            {
                declaration.register(context);
            }
        }

        public void check(Context context)
        {
            foreach (IDeclaration declaration in this)
            {
                declaration.check(context);
            }
        }

        public ConcreteMethodDeclaration findMain()
        {
            foreach (IDeclaration declaration in this)
            {
                if (!(declaration is ConcreteMethodDeclaration))
                    continue;
                ConcreteMethodDeclaration method = (ConcreteMethodDeclaration)declaration;
                if (!(method.getName().Equals("main")))
                    continue;
                // TODO check proto
                return method;
            }
            return null;
        }

		public void ToDialect(CodeWriter writer) {
			foreach(IDeclaration declaration in this) {
				declaration.ToDialect(writer);
				writer.append("\n");
			}
		}


    }

}