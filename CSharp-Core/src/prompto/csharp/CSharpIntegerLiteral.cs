using System;
using prompto.runtime;
using prompto.value;
using prompto.type;
namespace prompto.csharp
{

    public class CSharpIntegerLiteral : CSharpLiteral
    {

        IntegerValue value;

        public CSharpIntegerLiteral(String text)
			: base(text)
        {
            this.value = IntegerValue.Parse(text);
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