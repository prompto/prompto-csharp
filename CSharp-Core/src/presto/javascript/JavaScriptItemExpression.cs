using System;
using presto.utils;namespace presto.javascript{

public class JavaScriptItemExpression : JavaScriptSelectorExpression {

	JavaScriptExpression item;
	
	public JavaScriptItemExpression(JavaScriptExpression item) {
		this.item = item;
	}

	override
	public String ToString() {
			return parent.ToString() + "[" + item.ToString() + "]";
	}
	
	override
	public void ToDialect(CodeWriter writer) {
			parent.ToDialect(writer);
		writer.append('[');
			item.ToDialect(writer);
		writer.append(']');
	}

}

}
