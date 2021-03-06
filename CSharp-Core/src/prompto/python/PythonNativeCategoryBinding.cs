using prompto.grammar;
using prompto.utils;
using System;

namespace prompto.python
{

	public class PythonNativeCategoryBinding : NativeCategoryBinding
    {

		String identifier;
		PythonModule module;

		public PythonNativeCategoryBinding(String identifier, PythonModule module) {
			this.identifier = identifier;
			this.module = module;
		}

		public PythonNativeCategoryBinding(PythonNativeCategoryBinding binding) {
			this.identifier = binding.identifier;
			this.module = binding.module;
		}

		override
		public void ToDialect(CodeWriter writer) {
			writer.append(identifier);
			if(module!=null)
				module.ToDialect(writer);
		}
	}
}
