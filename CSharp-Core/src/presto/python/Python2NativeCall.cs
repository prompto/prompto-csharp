using presto.utils;namespace presto.python {

    public class Python2NativeCall : PythonNativeCall
    {

        public Python2NativeCall(PythonStatement statement)
            : base(statement)
        {
        }

		override
		public void ToDialect(CodeWriter writer) {
			writer.append("Python2: ");
			base.ToDialect(writer);
		}
    }
}
