// generated: 2015-07-05T23:01:01.219
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestDiv : BaseEParserTest
	{

		[Test]
		public void testDivDecimal()
		{
			compareResourceEOE("div/divDecimal.pec");
		}

		[Test]
		public void testDivInteger()
		{
			compareResourceEOE("div/divInteger.pec");
		}

		[Test]
		public void testIdivInteger()
		{
			compareResourceEOE("div/idivInteger.pec");
		}

		[Test]
		public void testModInteger()
		{
			compareResourceEOE("div/modInteger.pec");
		}

	}
}

