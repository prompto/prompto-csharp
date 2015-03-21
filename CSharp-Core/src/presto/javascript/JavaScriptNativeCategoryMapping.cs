using presto.grammar;
using System;
using presto.utils;

namespace presto.javascript {

public class JavaScriptNativeCategoryMapping : NativeCategoryMapping {

	String identifier;
	JavaScriptModule module;
	
	public JavaScriptNativeCategoryMapping(String identifier, JavaScriptModule module) {
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