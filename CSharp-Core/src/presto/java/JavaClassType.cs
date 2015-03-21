using presto.grammar;
using System;
using presto.type;

namespace presto.java
{

    class JavaClassType : CategoryType
    {

        public JavaClassType(String name, System.Type klass)
            : base(name)
        {
        }
    }
}