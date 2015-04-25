using presto.grammar;

namespace presto.declaration
{

	public class AnyNativeCategoryDeclaration : NativeCategoryDeclaration
	{

		static private AnyNativeCategoryDeclaration instance = new AnyNativeCategoryDeclaration ();

		static public AnyNativeCategoryDeclaration Instance {
			get {
				return instance;
			}
		}

		private AnyNativeCategoryDeclaration ()
			: base ("Any", new IdentifierList (), 
				new NativeCategoryBindingList (), 
				new NativeAttributeBindingListMap (),
				new MethodDeclarationList())
		{
		}
	}

}
