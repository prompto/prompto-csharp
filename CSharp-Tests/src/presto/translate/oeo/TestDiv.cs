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
			compareResourceOEO("div/divDecimal.o");
		}

		[Test]
		public void testDivInteger()
		{
			compareResourceOEO("div/divInteger.o");
		}

		[Test]
		public void testIdivInteger()
		{
			compareResourceOEO("div/idivInteger.o");
		}

		[Test]
		public void testModInteger()
		{
			compareResourceOEO("div/modInteger.o");
		}

	}
}

