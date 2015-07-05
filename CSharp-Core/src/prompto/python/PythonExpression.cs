using prompto.utils;

namespace prompto.python {

    public interface PythonExpression
    {
		void ToDialect(CodeWriter writer);
    }
}
