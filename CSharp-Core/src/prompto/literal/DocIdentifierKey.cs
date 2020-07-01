using System;
using prompto.runtime;
using prompto.utils;
using prompto.value;

namespace prompto.literal
{
	public class DocIdentifierKey : DocKey
	{
		String id;

		public DocIdentifierKey(String id)
		{
			this.id = id;
		}

        internal override void ToDialect(CodeWriter writer)
        {
			writer.append(this.id);
        }

        internal override string interpret(Context context)
        {
            return id;
		}

		public override string ToString()
		{
			return this.id;
		}
	}
}
