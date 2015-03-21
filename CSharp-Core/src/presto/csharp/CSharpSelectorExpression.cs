using presto.runtime;
using presto.type;
using presto.utils;


namespace presto.csharp
{

    public abstract class CSharpSelectorExpression : CSharpExpression
    {

        protected CSharpExpression parent;

        public void SetParent(CSharpExpression parent)
        {
            this.parent = parent;
        }

		public abstract IType check(Context context);
		public abstract object interpret(Context context);
		public abstract void ToDialect(CodeWriter writer);

    }
}
