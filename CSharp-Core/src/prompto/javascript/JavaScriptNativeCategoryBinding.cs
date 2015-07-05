using prompto.grammar;
using System;
using prompto.utils;

namespace prompto.javascript {

public class JavaScriptNativeCategoryBinding : NativeCategoryBinding {

	String identifier;
	JavaScriptModule module;
	
	public JavaScriptNativeCategoryBinding(String identifier, JavaScriptModule module) {
		this.identifier = identifier;
		this.module = module;
	}
	
	override
	public void ToDialect(CodeWriter writer) {
		writer.append("JavaScript: ");
		writer.append(identifier);
		if(module!=null)
			module.toDialect(writer);
	}

}
}