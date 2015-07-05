using System;
using prompto.utils;


namespace prompto.python {

    public class PythonIdentifierExpression : PythonExpression
    {

        public static PythonIdentifierExpression parse(String ids) {
		String[] parts = ids.Split(".".ToCharArray());
		PythonIdentifierExpression result = null;
		foreach (String part in parts)
			result = new PythonIdentifierExpression(result,part);
		return result;
	}

        PythonIdentifierExpression parent;
        String identifier;

        public PythonIdentifierExpression(String identifier)
        {
            this.identifier = identifier;
        }

        public PythonIdentifierExpression(PythonIdentifierExpression parent, String identifier)
        {
            this.parent = parent;
            this.identifier = identifier;
        }

        public PythonIdentifierExpression getParent()
        {
            return parent;
        }

        public String getIdentifier()
        {
            return identifier;
        }

        public override String ToString()
        {
            if (parent == null)
                return identifier;
            else
                return parent.ToString() + "." + identifier;
        }

		public void ToDialect(CodeWriter writer) {
			if(parent!=null) {
				parent.ToDialect(writer);
				writer.append('.');
			}
			writer.append(identifier);
		}
    }

}
