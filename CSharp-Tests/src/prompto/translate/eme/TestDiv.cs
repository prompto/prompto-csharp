using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestDiv : BaseEParserTest
	{

		[Test]
		public void testDivDecimal()
		{
			compareResourceEME("div/divDecimal.pec");
		}

		[Test]
		public void testDivInteger()
		{
			compareResourceEME("div/divInteger.pec");
		}

		[Test]
		public void testIdivInteger()
		{
			compareResourceEME("div/idivInteger.pec");
		}

		[Test]
		public void testModInteger()
		{
			compareResourceEME("div/modInteger.pec");
		}

	}
}

