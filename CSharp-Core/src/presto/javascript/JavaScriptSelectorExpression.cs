using presto.utils;namespace presto.javascript {


public abstract class JavaScriptSelectorExpression : JavaScriptExpression {

	protected JavaScriptExpression parent;
	
	public void setParent(JavaScriptExpression parent) {
		this.parent = parent;
	}

		public abstract void ToDialect(CodeWriter writer);
	
}

}
