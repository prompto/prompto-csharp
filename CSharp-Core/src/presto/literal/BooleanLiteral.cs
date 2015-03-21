using presto.runtime;
using System;
using Boolean = presto.value.Boolean;
using presto.type;
namespace presto.literal {

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
