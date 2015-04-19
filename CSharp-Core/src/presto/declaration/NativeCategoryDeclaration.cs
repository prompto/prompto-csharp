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

        NativeCategoryBindingList categoryBindings;
        // NativeAttributeBindingListMap attributeBindings;
        Type mappedClass = null;

        public NativeCategoryDeclaration(String name, IdentifierList attributes,
                NativeCategoryBindingList categoryBindings, NativeAttributeBindingListMap attributeBindings)
            : base(name, attributes)
        {
            this.categoryBindings = categoryBindings;
            // this.attributeBindings = attributeBindings;
        }

 
		public override void register (Context context)
		{
			base.register (context);
			Type type = getMappedClass (false);
			if(type!=null)
				context.registerNativeBinding (type, this);
		}

		protected override void toEDialect(CodeWriter writer) {
			protoToEDialect(writer, false, true);
			bindingsToEDialect(writer);
		}

		protected override void categoryTypeToEDialect(CodeWriter writer) {
			writer.append("native category");
		}

		protected void bindingsToEDialect(CodeWriter writer) {
			writer.indent();
			categoryBindings.ToDialect(writer);
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
			categoryBindings.ToDialect(writer);
		}

		protected override void toPDialect(CodeWriter writer) {
			protoToPDialect(writer, null);
			writer.indent();
			writer.newLine();
			categoryBindings.ToDialect(writer);
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
                CSharpNativeCategoryBinding binding = getBinding(fail); // TODO
				if (binding != null) 
				{
					mappedClass = binding.getExpression ().interpret_type ();
					if (mappedClass == null && fail)
						throw new SyntaxError ("No CSharp class:" + binding.getExpression ().ToString ());
				}
            }
            return mappedClass;
        }

        private CSharpNativeCategoryBinding getBinding(bool fail)
        {
            foreach (NativeCategoryBinding binding in categoryBindings)
            {
                if (binding is CSharpNativeCategoryBinding)
                    return (CSharpNativeCategoryBinding)binding;
            }
			if (fail)
				throw new SyntaxError ("Missing CSharp binding !");
			else
				return null;
        }

    }

}