using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestDiv : BaseOParserTest
	{

		[Test]
		public void testDivDecimal()
		{
			compareResourceOSO("div/divDecimal.poc");
		}

		[Test]
		public void testDivInteger()
		{
			compareResourceOSO("div/divInteger.poc");
		}

		[Test]
		public void testIdivInteger()
		{
			compareResourceOSO("div/idivInteger.poc");
		}

		[Test]
		public void testModInteger()
		{
			compareResourceOSO("div/modInteger.poc");
		}

	}
}

