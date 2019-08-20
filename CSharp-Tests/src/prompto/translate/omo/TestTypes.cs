using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestTypes : BaseOParserTest
	{

		[Test]
		public void testLiteral()
		{
			compareResourceOMO("types/literal.poc");
		}

	}
}

