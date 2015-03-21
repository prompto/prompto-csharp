using presto.utils;

namespace presto.javascript
{

	public class JavaScriptExpressionList : ObjectList<JavaScriptExpression>
	{

		public JavaScriptExpressionList ()
		{
		}

		public JavaScriptExpressionList (JavaScriptExpression expression)
		{
			this.add (expression);
		}

		public void toDialect (CodeWriter writer)
		{
			if (this.Count > 0) 
			{
				foreach (JavaScriptExpression exp in this) 
				{
					exp.ToDialect (writer);
					writer.append (", ");
				}
				writer.trimLast (2);
			}
		}

	}
}
