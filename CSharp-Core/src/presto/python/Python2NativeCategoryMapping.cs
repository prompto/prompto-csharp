using presto.grammar;
using presto.utils;

namespace presto.python
{

	public class Python2NativeCategoryBinding : PythonNativeCategoryBinding
    {

		public Python2NativeCategoryBinding(PythonNativeCategoryBinding binding)
			: base(binding)
        {
        }

		override
		public void ToDialect(CodeWriter writer) {
			writer.append("Python2: ");
			base.ToDialect(writer);
		}
	}
}
