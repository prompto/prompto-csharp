using System;
using prompto.runtime;
using prompto.type;
using System.Globalization;

namespace prompto.csharp
{

    public class CSharpDecimalLiteral : CSharpLiteral
    {

		Double value;

        public CSharpDecimalLiteral(String text)
			: base(text)
        {
			this.value = Double.Parse(text, CultureInfo.InvariantCulture);
        }

		override
	    public IType check(Context context) {
			return new CSharpClassType(typeof(Double));
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
