using prompto.utils;
using prompto.error;
using prompto.parser;
using prompto.runtime;
using System;
using prompto.type;
using prompto.declaration;
using prompto.grammar;

namespace prompto.argument {

public class MethodArgument : BaseArgument, INamedArgument {
	
	public MethodArgument(String name) 
    	: base(name)
	{
	}
	
	override
	public String getSignature(Dialect dialect) {
			return GetName();
	}

	override
	public void ToDialect(CodeWriter writer) {
			writer.append(GetName());
	}
	
	override
	public String getProto(Context context) {
			return GetName();
	}
	
	override
	public bool Equals(Object obj) {
		if(obj==this)
			return true;
		if(obj==null)
			return false;
		if(!(obj is MethodArgument))
			return false;
		MethodArgument other = (MethodArgument)obj;
			return Utils.equal(this.GetName(),other.GetName());
	}

	override
	public void register(Context context) {
        INamed actual = context.getRegisteredValue<INamed>(name);
		if(actual!=null)
			throw new SyntaxError("Duplicate argument: \"" + name + "\"");
		context.registerValue(this);
	}
	
	override
	public void check(Context context) {
        IMethodDeclaration actual = context.getRegisteredDeclaration<IMethodDeclaration>(name);
		if(actual==null)
			throw new SyntaxError("Unknown method: \"" + name + "\"");
	}
	
	override
	public IType GetIType(Context context) {
		return new MethodType(context,name);
	}
	
}

}