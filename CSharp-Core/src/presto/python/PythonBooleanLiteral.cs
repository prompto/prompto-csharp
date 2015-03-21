using Boolean = presto.value.Boolean;
using System;

namespace presto.python {

    public class PythonBooleanLiteral : PythonLiteral
    {

        public PythonBooleanLiteral(String text)
			: base(text)
        {
        }
    }
}
