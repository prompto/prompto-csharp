using presto.utils;

namespace presto.csharp
{

    public class CSharpExpressionList : ObjectList<CSharpExpression>
    {

        public CSharpExpressionList()
        {
        }

        public CSharpExpressionList(CSharpExpression expression)
        {
            this.Add(expression);
        }

		public void ToDialect(CodeWriter writer) {
			if(this.Count>0) {
				foreach(CSharpExpression exp in this) {
					exp.ToDialect(writer);
					writer.append(", ");
				}
				writer.trimLast(2);
			}
		}


    }
}