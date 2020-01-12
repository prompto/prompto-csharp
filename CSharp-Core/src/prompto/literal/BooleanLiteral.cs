using prompto.runtime;
using System;
using BooleanValue = prompto.value.BooleanValue;
using prompto.type;
namespace prompto.literal {

public class BooleanLiteral : Literal<BooleanValue> {

	public BooleanLiteral(String text) 
		: base(text, BooleanValue.Parse(text))
    {
	}
	
    override
	public IType check(Context context) {
		return BooleanType.Instance;
	}
	
}

}
