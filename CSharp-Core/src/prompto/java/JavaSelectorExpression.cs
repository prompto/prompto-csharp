using prompto.type;
using prompto.utils;


namespace prompto.java
{


    public abstract class JavaSelectorExpression : JavaExpression
    {

        protected JavaExpression parent;

        public void SetParent(JavaExpression parent)
        {
            this.parent = parent;
        }

     }

}
