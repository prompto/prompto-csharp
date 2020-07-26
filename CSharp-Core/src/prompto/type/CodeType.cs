using System;
using prompto.runtime;
using prompto.store;

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
	
	
    public override Type ToCSharpType(Context context) {
		return null;
	}
}

}
