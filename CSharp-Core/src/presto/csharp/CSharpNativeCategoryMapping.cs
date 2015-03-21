using presto.grammar;
using presto.utils;

namespace presto.csharp
{

	public class CSharpNativeCategoryMapping : NativeCategoryMapping
    {
        CSharpIdentifierExpression expression;

        public CSharpNativeCategoryMapping(CSharpIdentifierExpression expression)
        {
            this.expression = expression;
        }

        public CSharpIdentifierExpression getExpression()
        {
            return expression;
        }

		override
		public void ToDialect(CodeWriter writer) {
			writer.append("C#: ");
			expression.ToDialect(writer);
		}

    }

}
