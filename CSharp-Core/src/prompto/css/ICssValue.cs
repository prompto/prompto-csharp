using prompto.utils;

namespace prompto.css
{

	public interface ICssValue
	{
		void ToDialect(CodeWriter writer);
	}
}
