using presto.error;
using presto.grammar;
using presto.runtime;
using System;
using System.Reflection;
using presto.type;
using presto.utils;

namespace presto.java
{

    public class JavaIdentifierExpression : JavaExpression
    {

        public static JavaIdentifierExpression parse(String ids) {
		String[] parts = ids.Split("\\.".ToCharArray());
		JavaIdentifierExpression result = null;
		foreach(String part in parts)
			result = new JavaIdentifierExpression(result,part);
		return result;
	}

        JavaIdentifierExpression parent = null;
        String identifier;
        bool isChildClass = false;

        public JavaIdentifierExpression(String identifier)
        {
            this.identifier = identifier;
        }

        public JavaIdentifierExpression(JavaIdentifierExpression parent, String identifier)
        {
            this.parent = parent;
            this.identifier = identifier;
        }

        public JavaIdentifierExpression(JavaIdentifierExpression parent, String identifier, bool isChildClass)
        {
            this.parent = parent;
            this.identifier = identifier;
            this.isChildClass = isChildClass;
        }

        public JavaIdentifierExpression getParent()
        {
            return parent;
        }

        public String getIdentifier()
        {
            return identifier;
        }

        override
        public String ToString()
        {
            if (parent == null)
                return identifier;
            else
                return parent.ToString() + (isChildClass ? '$' : '.') + identifier;
        }

		public void ToDialect(CodeWriter writer) {
			if(parent!=null) {
				parent.ToDialect(writer);
				writer.append(isChildClass ? '$' : '.');
			}
			writer.append(identifier);
		}

		public Object interpret(Context context)
        {
            return null; // TODO
        }


        public IType check(Context context)
        {
            return null; // TODO
        }

     

    }
}