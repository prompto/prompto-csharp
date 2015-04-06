using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
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

