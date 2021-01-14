using prompto.runtime;
using System;
using prompto.store;

namespace prompto.type
{

public class VoidType : NativeType {

	static VoidType instance = new VoidType();
	
	public static VoidType Instance {
		get 
        {
            return instance;
        }
	}
	
	private VoidType() 
			: base(TypeFamily.VOID)
	{
	}
	
	override
	public System.Type ToCSharpType(Context context) {
		return typeof(void);
	}

	
	public override bool isAssignableFrom(Context context, IType other) {
		// illegal, but happens during syntax checking, if error is collected rather than thrown
		return false;
	}
	
}

}
