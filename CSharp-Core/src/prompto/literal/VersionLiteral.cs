using prompto.value;
using System;
using prompto.runtime;
using prompto.type;
namespace prompto.literal
{

    public class VersionLiteral : Literal<value.VersionValue>
    {

        public VersionLiteral(String text)
            : base(text, parseVersion(text.Substring(2, text.Length - 3)))
        {
        }

        override
        public IType check(Context context)
        {
            return VersionType.Instance;
        }

        public static value.VersionValue parseVersion(String text)
        {
            return prompto.value.VersionValue.Parse(text);
        }

    }
	
	
}
