using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestDiv : BaseOParserTest
	{

		[Test]
		public void testDivDecimal()
		{
			compareResourceOPO("div/divDecimal.o");
		}

		[Test]
		public void testDivInteger()
		{
			compareResourceOPO("div/divInteger.o");
		}

		[Test]
		public void testIdivInteger()
		{
			compareResourceOPO("div/idivInteger.o");
		}

		[Test]
		public void testModInteger()
		{
			compareResourceOPO("div/modInteger.o");
		}

	}
}

