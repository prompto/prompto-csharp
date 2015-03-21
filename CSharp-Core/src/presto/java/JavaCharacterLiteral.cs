using System;
using presto.runtime;
using Character = presto.value.Character;
using presto.value;
using presto.type;

namespace presto.java
{

    public class JavaCharacterLiteral : JavaLiteral
    {

        public JavaCharacterLiteral(String text)
			: base(text)
        {
        }

    }
}
