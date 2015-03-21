using presto.grammar;
using presto.utils;
using System;

namespace presto.python
{

	public class PythonNativeCategoryMapping : NativeCategoryMapping
    {

		String identifier;
		PythonModule module;

		public PythonNativeCategoryMapping(String identifier, PythonModule module) {
			this.identifier = identifier;
			this.module = module;
		}

		public PythonNativeCategoryMapping(PythonNativeCategoryMapping mapping) {
			this.identifier = mapping.identifier;
			this.module = mapping.module;
		}

		override
		public void ToDialect(CodeWriter writer) {
			writer.append(identifier);
			if(module!=null)
				module.ToDialect(writer);
		}
	}
}
