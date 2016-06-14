using prompto.runtime;
using System;
using prompto.value;
using prompto.type;

namespace prompto.literal
{

    public class UUIDLiteral : Literal<UUIDValue>
    {

        public UUIDLiteral(String text)
            : base(text, new UUIDValue(Parse(text)))
        {
        }

        private static Guid Parse(String text)
        {
			return Guid.Parse(text.Substring(1, text.Length - 2));
        }

        override
        public IType check(Context context)
        {
            return UUIDType.Instance;
        }

    }

}
