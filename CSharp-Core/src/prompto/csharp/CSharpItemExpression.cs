using System;
using prompto.runtime;
using prompto.type;
using prompto.utils;


namespace prompto.csharp
{

    public class CSharpItemExpression : CSharpSelectorExpression
    {

        CSharpExpression item;

        public CSharpItemExpression(CSharpExpression item)
        {
            this.item = item;
        }

		override
        public IType check(Context context)
        {
            // TODO Auto-generated method stub
            return null;
        }

		override
        public Object interpret(Context context)
        {
            // TODO Auto-generated method stub
            return null;
        }

        override
        public String ToString()
        {
            return parent.ToString() + "[" + item.ToString() + "]";
        }

		override
		public void ToDialect(CodeWriter writer) {
			parent.ToDialect(writer);
			writer.append('[');
			item.ToDialect(writer);
			writer.append(']');
		}

    }
}
