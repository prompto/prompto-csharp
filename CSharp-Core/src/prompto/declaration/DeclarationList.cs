using prompto.runtime;
using System.Collections.Generic;
using prompto.declaration;
using prompto.utils;
using prompto.grammar;
using System.Linq;
using prompto.statement;

namespace prompto.declaration
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
			// register attributes first
			registerAttributes(context);
			// ok now
			registerCategories(context);
			registerEnumerated(context);
			registerMethods(context);
			registerTests(context);
       }

		public void registerAttributes(Context context)
		{
			foreach (IDeclaration d in this.Where(d => d is AttributeDeclaration))
			{
				d.register(context);
			}
		}

		public void registerCategories(Context context)
		{
			foreach (IDeclaration d in this.Where(d => d is CategoryDeclaration))
			{
				d.register(context);
			}
		}

		public void registerEnumerated(Context context)
		{
			foreach (IDeclaration d in this.Where(d => d is EnumeratedNativeDeclaration))
			{
				d.register(context);
			}
		}

		public void registerMethods(Context context)
		{
			foreach (IDeclaration d in this.Where(d => d is IMethodDeclaration))
			{
				d.register(context);
			}
		}

		public void registerTests(Context context)
		{
			foreach (IDeclaration d in this.Where(d => d is TestMethodDeclaration))
			{
				d.register(context);
			}
		}

		public void check(Context context)
        {
            foreach (IDeclaration declaration in this)
            {
				if (declaration is IMethodDeclaration)
					((IMethodDeclaration)declaration).check(context, true);
                else
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
				if (!(method.GetName().Equals("main")))
                    continue;
                // TODO check proto
                return method;
            }
            return null;
        }

		public void ToDialect(CodeWriter writer) {
			foreach(IDeclaration declaration in this) {
				if(declaration.Comments!=null) {
					foreach(CommentStatement comment in declaration.Comments)
						comment.ToDialect(writer);
				}
				if(declaration.Annotations!=null) {
					foreach(Annotation annotation in declaration.Annotations)
						annotation.ToDialect(writer);
				}
				declaration.ToDialect(writer);
				writer.append("\n");
			}
		}


    }

}