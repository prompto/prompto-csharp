using System;
using prompto.utils;

namespace prompto.javascript {

public class JavaScriptMethodExpression : JavaScriptSelectorExpression {

	String name;
	JavaScriptExpressionList arguments = new JavaScriptExpressionList();
	
	public JavaScriptMethodExpression(String name) {
		this.name = name;
	}

	public void setArguments(JavaScriptExpressionList l1) {
		this.arguments = l1;
	}

	override
	public String ToString() {
		if(parent!=null)
				return parent.ToString() + "." + name + "(" + arguments.ToString() + ")";
		else
				return name + "(" + arguments.ToString() + ")";
	}
	
	override
	public void ToDialect(CodeWriter writer) {
		if(parent!=null) {
				parent.ToDialect(writer);
			writer.append('.');
		}
		writer.append(name);
		writer.append('(');
		if(arguments!=null)
			arguments.toDialect(writer);
		writer.append(')');
	}
	
}

}