using System;
using presto.runtime;
using presto.value;
using presto.type;
namespace presto.csharp
{

    public class CSharpIntegerLiteral : CSharpLiteral
    {

        Integer value;

        public CSharpIntegerLiteral(String text)
			: base(text)
        {
            this.value = Integer.Parse(text);
        }

		override
 	    public IType check(Context context) {
			return new CSharpClassType(typeof(Int64));
	    }

		override
        public object interpret(Context context)
        {
            return value;
        }

        override
        public String ToString()
        {
            return value.ToString();
        }
    }
}