using presto.grammar;
using presto.utils;

namespace presto.java
{

    public class JavaNativeCategoryMapping : NativeCategoryMapping
    {

        JavaIdentifierExpression expression;

        public JavaNativeCategoryMapping(JavaIdentifierExpression expression)
        {
            this.expression = expression;
        }

        public JavaIdentifierExpression getExpression()
        {
            return expression;
        }

		override
		public void ToDialect(CodeWriter writer) {
			writer.append("Java: ");
			expression.ToDialect(writer);
		}

    }
}
