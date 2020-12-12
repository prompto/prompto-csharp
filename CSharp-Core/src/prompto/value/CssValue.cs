using prompto.css;
using prompto.runtime;
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

        public override IValue Add(Context context, IValue value)
        {
			if (value is CssValue)
				return new CssValue(expression.Plus(((CssValue)value).expression));
			else
				return base.Add(context, value);
        }

        public override string ToString()
        {
            return expression.ToString();
        }

    }

}
