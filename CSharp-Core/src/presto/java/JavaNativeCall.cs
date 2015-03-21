
using System;
using presto.runtime;
using presto.parser;
using presto.grammar;
using presto.type;
using presto.utils;
using presto.value;



namespace presto.java {

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
