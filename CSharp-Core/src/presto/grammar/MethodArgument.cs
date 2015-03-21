using presto.utils;
using presto.error;
using presto.parser;
using presto.runtime;
using System;
using presto.type;
using presto.declaration;

namespace presto.grammar {

public class MethodArgument : BaseArgument, INamedArgument {
	
	public MethodArgument(String name) 
    	: base(name)
	{
	}
	
	override
	public String getSignature(Dialect dialect) {
		return getName();
	}

	override
	public void ToDialect(CodeWriter writer) {
			writer.append(getName());
	}
	
	override
	public String getProto(Context context) {
		return getName();
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
		return Utils.equal(this.getName(),other.getName());
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
	public IType GetType(Context context) {
		return new MethodType(context,name);
	}
	
}

}