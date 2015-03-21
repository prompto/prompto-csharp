using presto.grammar;
using presto.utils;

namespace presto.python
{

	public class Python2NativeCategoryMapping : PythonNativeCategoryMapping
    {

		public Python2NativeCategoryMapping(PythonNativeCategoryMapping mapping)
			: base(mapping)
        {
        }

		override
		public void ToDialect(CodeWriter writer) {
			writer.append("Python2: ");
			base.ToDialect(writer);
		}
	}
}
