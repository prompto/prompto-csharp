using System;
using presto.utils;

namespace presto.python
{
	public interface PythonArgument {

		void ToDialect(CodeWriter writer);

	}
}

