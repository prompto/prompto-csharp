using System;
using prompto.grammar;
using prompto.runtime;
using prompto.utils;

namespace prompto.declaration
{
	public class SingletonCategoryDeclaration : ConcreteCategoryDeclaration
	{

		public SingletonCategoryDeclaration (String name, IdentifierList attributes, MethodDeclarationList methods)
			: base (name, attributes, null, methods)
		{
		}

		protected override void categoryTypeToEDialect (CodeWriter writer)
		{
			writer.append ("singleton");
		}

		protected override void categoryTypeToODialect (CodeWriter writer)
		{
			writer.append ("singleton");
		}

		protected override void categoryTypeToMDialect (CodeWriter writer)
		{
			writer.append ("singleton");
		}

        public ConcreteMethodDeclaration getInitializeMethod(Context context)
        {
			registerMethods(context);
			if(methodsMap.ContainsKey("initialize"))
            {
				IDeclaration decl = methodsMap["initialize"];
				if (decl is MethodDeclarationMap) {
					IMethodDeclaration method = ((MethodDeclarationMap)decl).GetFirst();
					if (method is ConcreteMethodDeclaration)
						return (ConcreteMethodDeclaration)method;
				}

			}
			return null;
        }
    }
}