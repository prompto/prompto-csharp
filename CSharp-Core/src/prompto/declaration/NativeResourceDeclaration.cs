using prompto.error;
using prompto.runtime;
using prompto.grammar;
using prompto.type;
using prompto.value;
using System;
using prompto.utils;

namespace prompto.declaration
{

	public class NativeResourceDeclaration : NativeCategoryDeclaration
	{

		public NativeResourceDeclaration (String name, 
		                                       IdentifierList attributes,
		                                       NativeCategoryBindingList categoryBindings, 
		                                       NativeAttributeBindingListMap attributeBindings,
		                                       MethodDeclarationList methods)
			: base (name, attributes, categoryBindings, attributeBindings, methods)
		{
		}

		override
        public IType GetIType (Context context)
		{
			return new ResourceType (name);
		}

		override
        public IInstance newInstance ()
		{
			return new NativeResource (this);
		}

		override
        public void checkConstructorContext (Context context)
		{
			if (!(context is ResourceContext))
				throw new SyntaxError ("Not a resource context!");
		}

		protected override void categoryTypeToEDialect (CodeWriter writer)
		{
			writer.append ("native resource");
		}

		protected override void categoryTypeToODialect (CodeWriter writer)
		{
			writer.append ("native resource");
		}

		protected override void categoryTypeToSDialect (CodeWriter writer)
		{
			writer.append ("native resource");
		}
	}
}