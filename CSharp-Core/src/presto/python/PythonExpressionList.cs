using presto.utils;

namespace presto.python
{

    public class PythonExpressionList : ObjectList<PythonExpression>
    {

        public PythonExpressionList()
        {
        }

        public PythonExpressionList(PythonExpression expression)
        {
            this.Add(expression);
        }

    }
}
