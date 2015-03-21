using presto.type;
using presto.utils;


namespace presto.java
{


    public abstract class JavaSelectorExpression : JavaExpression
    {

        protected JavaExpression parent;

        public void SetParent(JavaExpression parent)
        {
            this.parent = parent;
        }

		public abstract void ToDialect (CodeWriter writer);
     }

}
