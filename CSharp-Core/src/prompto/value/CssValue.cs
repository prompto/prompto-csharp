using prompto.css;
using prompto.type;

namespace prompto.value
{
	#pragma warning disable 414
	public class CssValue : BaseValue
	{

		CssExpression expression;

		public CssValue(CssExpression expression)
				: base(CssType.Instance)
		{
			this.expression = expression;
		}

	}

}
