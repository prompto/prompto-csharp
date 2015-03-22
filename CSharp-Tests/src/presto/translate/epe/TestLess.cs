using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestLess : BaseEParserTest
	{

		[Test]
		public void testLtCharacter()
		{
			compareResourceEPE("less/ltCharacter.e");
		}

		[Test]
		public void testLtDate()
		{
			compareResourceEPE("less/ltDate.e");
		}

		[Test]
		public void testLtDateTime()
		{
			compareResourceEPE("less/ltDateTime.e");
		}

		[Test]
		public void testLtDecimal()
		{
			compareResourceEPE("less/ltDecimal.e");
		}

		[Test]
		public void testLteCharacter()
		{
			compareResourceEPE("less/lteCharacter.e");
		}

		[Test]
		public void testLteDate()
		{
			compareResourceEPE("less/lteDate.e");
		}

		[Test]
		public void testLteDateTime()
		{
			compareResourceEPE("less/lteDateTime.e");
		}

		[Test]
		public void testLteDecimal()
		{
			compareResourceEPE("less/lteDecimal.e");
		}

		[Test]
		public void testLteInteger()
		{
			compareResourceEPE("less/lteInteger.e");
		}

		[Test]
		public void testLteText()
		{
			compareResourceEPE("less/lteText.e");
		}

		[Test]
		public void testLteTime()
		{
			compareResourceEPE("less/lteTime.e");
		}

		[Test]
		public void testLtInteger()
		{
			compareResourceEPE("less/ltInteger.e");
		}

		[Test]
		public void testLtText()
		{
			compareResourceEPE("less/ltText.e");
		}

		[Test]
		public void testLtTime()
		{
			compareResourceEPE("less/ltTime.e");
		}

	}
}

