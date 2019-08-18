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

		
	    public override IType check(Context context) {
			return new CSharpClassType(typeof(Double));
	    }

		
        public override object interpret(Context context)
        {
            return value;
        }

        
        public override String ToString()
        {
            return value.ToString();
        }
    }

}
