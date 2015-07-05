using System;
using prompto.utils;

namespace prompto.python
{
	public interface PythonArgument {

		void ToDialect(CodeWriter writer);

	}
}

