using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestDiv : BaseEParserTest
	{

		[Test]
		public void testDivDecimal()
		{
			compareResourceEOE("div/divDecimal.e");
		}

		[Test]
		public void testDivInteger()
		{
			compareResourceEOE("div/divInteger.e");
		}

		[Test]
		public void testIdivInteger()
		{
			compareResourceEOE("div/idivInteger.e");
		}

		[Test]
		public void testModInteger()
		{
			compareResourceEOE("div/modInteger.e");
		}

	}
}

