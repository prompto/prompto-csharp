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

		public override IType check(Context context)
        {
            // TODO Auto-generated method stub
            return null;
        }

		public override Object interpret(Context context)
        {
            // TODO Auto-generated method stub
            return null;
        }

        public override String ToString()
        {
            return parent.ToString() + "[" + item.ToString() + "]";
        }

		public override void ToDialect(CodeWriter writer) {
			parent.ToDialect(writer);
			writer.append('[');
			item.ToDialect(writer);
			writer.append(']');
		}

    }
}
