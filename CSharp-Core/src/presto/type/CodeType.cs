using System;
namespace presto.type
{


public class CodeType : NativeType {

    static CodeType instance_ = new CodeType();

    
    public static CodeType Instance
    {
        get
        {
            return instance_;
        }
	}
	
	private CodeType() 
 		: base("Code")
   {
	}
	
	override
    public Type ToSystemType() {
		return null;
	}
}

}
