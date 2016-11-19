using prompto.declaration;
using System;

namespace prompto.parser
{

	public abstract class BaseMParserTest  : BaseParserTest
    {

		public DeclarationList parseString(String code)
		{
			return parseMString(code);
		}

		override
		public DeclarationList parseResource(String resourceName)
		{
			return parseMResource(resourceName);
		}
    }
}