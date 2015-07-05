using prompto.utils;

namespace prompto.python
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
