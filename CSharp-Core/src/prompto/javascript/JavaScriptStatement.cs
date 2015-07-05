using System;
using prompto.utils;

namespace prompto.javascript {

public class JavaScriptStatement {

	JavaScriptExpression expression;
	JavaScriptModule module;
	bool isReturn;
	
	public JavaScriptStatement(JavaScriptExpression expression,bool isReturn) {
		this.expression = expression;
		this.isReturn = isReturn;
	}

	public void setModule(JavaScriptModule module) {
		this.module = module;
	}
	
	override
	public String ToString() {
			return "" + (isReturn ? "return " : "") + expression.ToString() + ";";
	}

	public void ToDialect(CodeWriter writer) {
		if(isReturn)
			writer.append("return ");
			expression.ToDialect(writer);
		writer.append(';');
		if(module!=null)
			module.toDialect(writer);
	}
}
}