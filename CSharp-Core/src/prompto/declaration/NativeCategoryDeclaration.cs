using System;

using prompto.error;
using prompto.runtime;
using prompto.grammar;
using prompto.value;
using prompto.csharp;
using prompto.utils;
using prompto.statement;

namespace prompto.declaration
{

	public class NativeCategoryDeclaration : ConcreteCategoryDeclaration
	{

		NativeCategoryBindingList categoryBindings;
		// NativeAttributeBindingListMap attributeBindings;
		Type boundClass = null;

		public NativeCategoryDeclaration (String name, 
		                                       IdentifierList attributes,
		                                       NativeCategoryBindingList categoryBindings, 
		                                       NativeAttributeBindingListMap attributeBindings,
		                                       MethodDeclarationList methods)
			: base (name, attributes, null, methods)
		{
			this.categoryBindings = categoryBindings;
			// this.attributeBindings = attributeBindings;
		}

 
		public override void register (Context context)
		{
			base.register (context);
			Type type = getBoundClass (false);
			if (type != null)
				context.registerNativeBinding (type, this);
		}

		protected override void ToEDialect (CodeWriter writer)
		{
			protoToEDialect (writer, false, true);
			bindingsToEDialect (writer);
			methodsToEDialect(writer);
		}

		private void methodsToEDialect(CodeWriter writer) {
			if(methods!=null && methods.Count>0) {
				writer.append("and methods:");
				writer.newLine();
				methodsToEDialect(writer, methods);
			}

		}

		protected override void categoryTypeToEDialect (CodeWriter writer)
		{
			writer.append ("native category");
		}

		protected void bindingsToEDialect (CodeWriter writer)
		{
			writer.indent ();
			categoryBindings.ToDialect (writer);
			writer.dedent ();
			writer.newLine ();
		}

		protected override void ToODialect (CodeWriter writer)
		{
			bool hasBody = true; // always one
			ToODialect (writer, hasBody); 
		}

		protected override void categoryTypeToODialect (CodeWriter writer)
		{
			if(this.Storable)
				writer.append("storable ");
			writer.append ("native category");
		}

		protected override void bodyToODialect (CodeWriter writer)
		{
			categoryBindings.ToDialect (writer);
			if(methods!=null && methods.Count>0) {
				writer.newLine();
				writer.newLine();
				methodsToODialect(writer, methods);
			}
		}

		protected override void ToMDialect (CodeWriter writer)
		{
			protoToMDialect (writer, null);
			writer.indent ();
			writer.newLine ();
			categoryBindings.ToDialect (writer);
			if(methods!=null && methods.Count>0)
			{
				foreach(IDeclaration decl in methods) {
					if (decl.Comments != null)
						foreach (CommentStatement comment in decl.Comments)
							comment.ToDialect(writer);
					if (decl.Annotations != null)
						foreach (Annotation annotation in decl.Annotations)
							annotation.ToDialect(writer);
					CodeWriter w = writer.newMemberWriter();
					decl.ToDialect(w);
					writer.newLine();
				}
			}
			writer.dedent ();
			writer.newLine ();
		}

		protected override void categoryTypeToMDialect (CodeWriter writer)
		{
			writer.append ("native category");
		}



        public override IInstance newInstance (Context context)
		{
			return new NativeInstance (this);
		}

		public System.Type getBoundClass (bool fail)
		{
			if (boundClass == null) {
				CSharpNativeCategoryBinding binding = getBinding (fail); // TODO
				if (binding != null) {
					boundClass = binding.getExpression ().interpret_type ();
					if (boundClass == null && fail)
						throw new SyntaxError ("No CSharp class:" + binding.getExpression ().ToString ());
				}
			}
			return boundClass;
		}

		private CSharpNativeCategoryBinding getBinding (bool fail)
		{
			foreach (NativeCategoryBinding binding in categoryBindings) {
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