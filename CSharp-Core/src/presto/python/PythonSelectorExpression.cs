using presto.utils;

namespace presto.python
{

    public abstract class PythonSelectorExpression : PythonExpression
    {

        protected PythonExpression parent;

        public void setParent(PythonExpression parent)
        {
            this.parent = parent;
        }

		public abstract void ToDialect(CodeWriter writer);

    }
}