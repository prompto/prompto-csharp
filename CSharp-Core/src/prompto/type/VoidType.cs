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
	public System.Type ToCSharpType() {
		return typeof(void);
	}

	override
	public bool isAssignableTo(Context context, IType other) {
		throw new Exception("Should never get there !");
	}
	
}

}
