using prompto.grammar;
using prompto.utils;

namespace prompto.csharp
{

	public class CSharpNativeCategoryBinding : NativeCategoryBinding
    {
        CSharpIdentifierExpression expression;

        public CSharpNativeCategoryBinding(CSharpIdentifierExpression expression)
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
