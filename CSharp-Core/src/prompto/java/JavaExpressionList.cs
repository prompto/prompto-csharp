using System;
using System.Text;
using prompto.utils;

namespace prompto.java {

    public class JavaExpressionList : ObjectList<JavaExpression>
    {

        public JavaExpressionList()
        {
        }

        public JavaExpressionList(JavaExpression expression)
        {
            this.Add(expression);
        }

        override
        public String ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (JavaExpression exp in this)
            {
                sb.Append(exp.ToString());
                sb.Append(", ");
            }
            sb.Length = sb.Length - 2;
            return sb.ToString();
        }

		public void ToDialect(CodeWriter writer) {
			if(this.Count>0) {
				foreach(JavaExpression exp in this) {
					exp.ToDialect(writer);
					writer.append(", ");
				}
				writer.trimLast(2);
			}
		}

    }
}
