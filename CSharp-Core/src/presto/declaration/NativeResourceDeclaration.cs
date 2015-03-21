using presto.error;
using presto.runtime;
using presto.grammar;
using presto.type;
using presto.value;
using System;
using presto.utils;

namespace presto.declaration
{

    public class NativeResourceDeclaration : NativeCategoryDeclaration
    {

        public NativeResourceDeclaration(String name, IdentifierList attributes,
                NativeCategoryMappingList categoryMappings, NativeAttributeMappingListMap attributeMappings)
            : base(name, attributes, categoryMappings, attributeMappings)
        {
        }

        override
        public IType GetType(Context context)
        {
            return new ResourceType(name);
        }

        override
        public IInstance newInstance()
        {
            return new NativeResource(this);
        }

        override
        public void checkConstructorContext(Context context)
        {
            if (!(context is ResourceContext))
                throw new SyntaxError("Not a resource context!");
        }

		protected override void categoryTypeToEDialect(CodeWriter writer) {
			writer.append("native resource");
		}

		protected override void categoryTypeToODialect(CodeWriter writer) {
			writer.append("native resource");
		}

		protected override void categoryTypeToPDialect(CodeWriter writer) {
			writer.append("native resource");
		}
    }
}