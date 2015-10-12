using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestDiv : BaseOParserTest
	{

		[Test]
		public void testDivDecimal()
		{
			compareResourceOEO("div/divDecimal.poc");
		}

		[Test]
		public void testDivInteger()
		{
			compareResourceOEO("div/divInteger.poc");
		}

		[Test]
		public void testIdivInteger()
		{
			compareResourceOEO("div/idivInteger.poc");
		}

		[Test]
		public void testModInteger()
		{
			compareResourceOEO("div/modInteger.poc");
		}

	}
}

