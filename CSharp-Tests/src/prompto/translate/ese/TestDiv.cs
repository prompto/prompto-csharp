using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestDiv : BaseEParserTest
	{

		[Test]
		public void testDivDecimal()
		{
			compareResourceESE("div/divDecimal.pec");
		}

		[Test]
		public void testDivInteger()
		{
			compareResourceESE("div/divInteger.pec");
		}

		[Test]
		public void testIdivInteger()
		{
			compareResourceESE("div/idivInteger.pec");
		}

		[Test]
		public void testModInteger()
		{
			compareResourceESE("div/modInteger.pec");
		}

	}
}

