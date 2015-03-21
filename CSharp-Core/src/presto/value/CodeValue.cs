using presto.expression;
using presto.type;
using presto.runtime;

namespace presto.value
{

	public class CodeValue : BaseValue
	{

		CodeExpression expression;

		public CodeValue (CodeExpression expression)
			: base(CodeType.Instance)
		{
			this.expression = expression;
		}

		public IType check (Context context)
		{
			return expression.checkCode (context);
		}

		public IValue interpret (Context context)
		{
			return expression.interpretCode (context);
		}
	}
}