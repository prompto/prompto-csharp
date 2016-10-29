using System;
namespace prompto.type
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
			: base(TypeFamily.CODE)
   {
	}
	
	override
    public Type ToCSharpType() {
		return null;
	}
}

}
