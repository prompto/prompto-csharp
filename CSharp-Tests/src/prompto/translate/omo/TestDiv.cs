using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestDiv : BaseOParserTest
	{

		[Test]
		public void testDivDecimal()
		{
			compareResourceOMO("div/divDecimal.poc");
		}

		[Test]
		public void testDivInteger()
		{
			compareResourceOMO("div/divInteger.poc");
		}

		[Test]
		public void testIdivInteger()
		{
			compareResourceOMO("div/idivInteger.poc");
		}

		[Test]
		public void testModInteger()
		{
			compareResourceOMO("div/modInteger.poc");
		}

	}
}

