using prompto.expression;

namespace prompto.utils
{
	public class ExpressionList : ObjectList<IExpression>
	{

		public ExpressionList ()
		{
		}

		public ExpressionList (IExpression item)
		{
			this.add (item);
		}

		public void toDialect (CodeWriter writer)
		{
			if (this.Count > 0) {
				foreach (IExpression exp in this) {
					exp.ToDialect (writer);
					writer.append (", ");
				}
				writer.trimLast (2);
			}
		}

	}
}