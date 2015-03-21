using System;
using System.Collections.Generic;
using presto.utils;

namespace presto.python
{
	public class PythonModule
	{
		List<String> identifiers;

		public PythonModule(List<String> identifiers) {
			this.identifiers = identifiers;
		}

		public void ToDialect(CodeWriter writer) {
			writer.append(" from module: ");
			foreach(String id in identifiers) {
				writer.append(id);
				writer.append('.');
			}
			writer.trimLast(1);
		}
	}
}

