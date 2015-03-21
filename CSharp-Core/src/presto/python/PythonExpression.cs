using presto.utils;

namespace presto.python {

    public interface PythonExpression
    {
		void ToDialect(CodeWriter writer);
    }
}
