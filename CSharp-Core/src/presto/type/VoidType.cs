using presto.runtime;
using System;

namespace presto.type
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
    	: base("Void")
	{
	}
	
	override
	public System.Type ToSystemType() {
		return typeof(void);
	}

	override
	public bool isAssignableTo(Context context, IType other) {
		throw new Exception("Should never get there !");
	}
	
}

}