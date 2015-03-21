using presto.grammar;
using presto.utils;

namespace presto.python
{

	public class Python3NativeCategoryMapping : PythonNativeCategoryMapping
	{

		public Python3NativeCategoryMapping(PythonNativeCategoryMapping mapping)
			: base(mapping)
		{
		}

		override
		public void ToDialect(CodeWriter writer) {
			writer.append("Python3: ");
			base.ToDialect(writer);
		}
	}

}
