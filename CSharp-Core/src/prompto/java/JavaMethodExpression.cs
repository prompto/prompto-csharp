using System;
using prompto.runtime;
using prompto.type;
using prompto.utils;


namespace prompto.java {

    public class JavaMethodExpression : JavaSelectorExpression
    {
        String name;
        JavaExpressionList arguments;

        public JavaMethodExpression(String name)
        {
            this.name = name;
        }

        public JavaMethodExpression(String name, JavaExpressionList arguments)
        {
            this.name = name;
			this.arguments = arguments!=null ? arguments : new JavaExpressionList();
        }

        public void addArgument(JavaExpression expression)
        {
            arguments.Add(expression);
        }

        override
        public String ToString()
        {
            return parent.ToString() + "." + name + "(" + arguments.ToString() + ")";
        }

		override
		public void ToDialect(CodeWriter writer) {
			parent.ToDialect(writer);
			writer.append('.');
			writer.append(name);
			writer.append('(');
			arguments.ToDialect(writer);
			writer.append(')');
		}


    }
}
