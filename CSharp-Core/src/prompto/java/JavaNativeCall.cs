
using System;
using prompto.runtime;
using prompto.parser;
using prompto.grammar;
using prompto.type;
using prompto.utils;
using prompto.value;
using prompto.statement;



namespace prompto.java {

public class JavaNativeCall : NativeCall {

	JavaStatement statement;
	
	public JavaNativeCall(JavaStatement statement) 
   {
		this.statement = statement;
	}
	
	override
    public void ToDialect(CodeWriter writer)
    {
		writer.append("Java: ");
		statement.ToDialect(writer);
	}
	
	override 
    public IType check(Context context) {
		return null;
	}
	
	override 
    public IValue interpret(Context context) {
		return null;
	}


}

}
