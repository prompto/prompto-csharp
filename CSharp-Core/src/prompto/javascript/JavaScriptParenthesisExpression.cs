using prompto.utils;namespace prompto.javascript {

public class JavaScriptParenthesisExpression : JavaScriptExpression {

	JavaScriptExpression expression;
	
	public JavaScriptParenthesisExpression(JavaScriptExpression expression) {
		this.expression = expression;
	}

	public void ToDialect(CodeWriter writer) {
		writer.append('(');
			expression.ToDialect(writer);
		writer.append(')');
	}
}
}
