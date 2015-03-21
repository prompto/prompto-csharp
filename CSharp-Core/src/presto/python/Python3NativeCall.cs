using presto.utils;

namespace presto.python
{

    public class Python3NativeCall : PythonNativeCall
    {

        public Python3NativeCall(PythonStatement statement)
            : base(statement)
        {
        }

		override
		public void ToDialect(CodeWriter writer) {
			writer.append("Python3: ");
			base.ToDialect(writer);
		}

    }
}
