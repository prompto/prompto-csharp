using System;

using presto.error;
using presto.runtime;
using System.Text;
using presto.parser;
using presto.grammar;
using presto.value;
using presto.csharp;
using presto.utils;

namespace presto.declaration
{

    public class NativeCategoryDeclaration : CategoryDeclaration
    {

        NativeCategoryMappingList categoryMappings;
        // NativeAttributeMappingListMap attributeMappings;
        Type mappedClass = null;

        public NativeCategoryDeclaration(String name, IdentifierList attributes,
                NativeCategoryMappingList categoryMappings, NativeAttributeMappingListMap attributeMappings)
            : base(name, attributes)
        {
            this.categoryMappings = categoryMappings;
            // this.attributeMappings = attributeMappings;
        }

 
		public override void register (Context context)
		{
			base.register (context);
			Type type = getMappedClass (false);
			if(type!=null)
				context.registerNativeMapping (type, this);
		}

		protected override void toEDialect(CodeWriter writer) {
			protoToEDialect(writer, false, true);
			mappingsToEDialect(writer);
		}

		protected override void categoryTypeToEDialect(CodeWriter writer) {
			writer.append("native category");
		}

		protected void mappingsToEDialect(CodeWriter writer) {
			writer.indent();
			categoryMappings.ToDialect(writer);
			writer.dedent();
			writer.newLine();
		}

		protected override void toODialect(CodeWriter writer) {
			bool hasBody = true; // always one
			toODialect(writer, hasBody); 
		}

		protected override void categoryTypeToODialect(CodeWriter writer) {
			writer.append("native category");
		}

		protected override void bodyToODialect(CodeWriter writer) {
			categoryMappings.ToDialect(writer);
		}

		protected override void toPDialect(CodeWriter writer) {
			protoToPDialect(writer, null);
			writer.indent();
			writer.newLine();
			categoryMappings.ToDialect(writer);
			writer.dedent();
			writer.newLine();
		}

		protected override void categoryTypeToPDialect(CodeWriter writer) {
			writer.append("native category");
		}


        override
        public IInstance newInstance()
        {
            return new NativeInstance(this);
        }

		public System.Type getMappedClass(bool fail)
        {
			if (mappedClass == null)
            {
                CSharpNativeCategoryMapping mapping = getMapping(fail); // TODO
				if (mapping != null) 
				{
					mappedClass = mapping.getExpression ().interpret_type ();
					if (mappedClass == null && fail)
						throw new SyntaxError ("No CSharp class:" + mapping.getExpression ().ToString ());
				}
            }
            return mappedClass;
        }

        private CSharpNativeCategoryMapping getMapping(bool fail)
        {
            foreach (NativeCategoryMapping mapping in categoryMappings)
            {
                if (mapping is CSharpNativeCategoryMapping)
                    return (CSharpNativeCategoryMapping)mapping;
            }
			if (fail)
				throw new SyntaxError ("Missing CSharp mapping !");
			else
				return null;
        }

    }

}