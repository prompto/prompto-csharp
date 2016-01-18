using prompto.utils;

namespace prompto.javascript
{

	public class JavaScriptNewExpression : JavaScriptExpression
	{

		JavaScriptMethodExpression method;

		public JavaScriptNewExpression (JavaScriptMethodExpression method)
		{
			this.method = method;
		}

		public void ToDialect (CodeWriter writer)
		{
			writer.append ("new ");
			method.ToDialect (writer);
		}
	}
}
