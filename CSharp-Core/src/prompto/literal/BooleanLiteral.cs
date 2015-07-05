using prompto.runtime;
using System;
using Boolean = prompto.value.Boolean;
using prompto.type;
namespace prompto.literal {

public class BooleanLiteral : Literal<Boolean> {

	public BooleanLiteral(String text) 
		: base(text, Boolean.Parse(text))
    {
	}
	
    override
	public IType check(Context context) {
		return BooleanType.Instance;
	}
	
}

}
