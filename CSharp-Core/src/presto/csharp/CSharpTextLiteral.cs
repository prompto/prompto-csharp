using System;
using presto.runtime;
using presto.type;
namespace presto.csharp
{

    public class CSharpTextLiteral : CSharpLiteral
    {

        String value;

        public CSharpTextLiteral(String text)
			: base(text)
        {
            this.value = text.Substring(1, text.Length - 2);
        }

        override
        public String ToString()
        {
            return "\"" + value + "\"";
        }

		override
        public IType check(Context context)
        {
			return new CSharpClassType(typeof(String));
        }

		override
        public Object interpret(Context context)
        {
            return value;
        }
    }

}
