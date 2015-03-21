using presto.utils;

namespace presto.javascript
{


	public interface JavaScriptExpression
	{

		void ToDialect (CodeWriter writer);
	
	}

}