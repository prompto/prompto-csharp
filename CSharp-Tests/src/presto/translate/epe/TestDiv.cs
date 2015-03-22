using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestDiv : BaseEParserTest
	{

		[Test]
		public void testDivDecimal()
		{
			compareResourceEPE("div/divDecimal.e");
		}

		[Test]
		public void testDivInteger()
		{
			compareResourceEPE("div/divInteger.e");
		}

		[Test]
		public void testIdivInteger()
		{
			compareResourceEPE("div/idivInteger.e");
		}

		[Test]
		public void testModInteger()
		{
			compareResourceEPE("div/modInteger.e");
		}

	}
}

