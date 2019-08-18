using prompto.runtime;
using prompto.type;
using prompto.utils;


namespace prompto.csharp
{

    public abstract class CSharpSelectorExpression : CSharpExpression
    {

        protected CSharpExpression parent;

        public void SetParent(CSharpExpression parent)
        {
            this.parent = parent;
        }


    }
}
