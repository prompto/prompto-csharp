using System;
using prompto.runtime;
using Character = prompto.value.CharacterValue;
using prompto.value;
using prompto.type;

namespace prompto.java
{

    public class JavaCharacterLiteral : JavaLiteral
    {

        public JavaCharacterLiteral(String text)
			: base(text)
        {
        }

    }
}
