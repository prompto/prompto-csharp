using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestTypes : BaseOParserTest
	{

		[Test]
		public void testLiteral()
		{
			compareResourceOEO("types/literal.poc");
		}

	}
}

