using prompto.grammar;
using prompto.utils;

namespace prompto.python
{

	public class Python3NativeCategoryBinding : PythonNativeCategoryBinding
	{

		public Python3NativeCategoryBinding(PythonNativeCategoryBinding binding)
			: base(binding)
		{
		}

		override
		public void ToDialect(CodeWriter writer) {
			writer.append("Python3: ");
			base.ToDialect(writer);
		}
	}

}
