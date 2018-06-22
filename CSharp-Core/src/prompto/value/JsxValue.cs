using prompto.jsx;
using prompto.type;

namespace prompto.value
{
	#pragma warning disable 414
	public class JsxValue : BaseValue
	{

		IJsxExpression expression;

		public JsxValue(IJsxExpression expression)
				: base(JsxType.Instance)
		{
			this.expression = expression;
		}

	}

}
