using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestLess : BaseEParserTest
	{

		[Test]
		public void testLtCharacter()
		{
			compareResourceEOE("less/ltCharacter.pec");
		}

		[Test]
		public void testLtDate()
		{
			compareResourceEOE("less/ltDate.pec");
		}

		[Test]
		public void testLtDateTime()
		{
			compareResourceEOE("less/ltDateTime.pec");
		}

		[Test]
		public void testLtDecimal()
		{
			compareResourceEOE("less/ltDecimal.pec");
		}

		[Test]
		public void testLteCharacter()
		{
			compareResourceEOE("less/lteCharacter.pec");
		}

		[Test]
		public void testLteDate()
		{
			compareResourceEOE("less/lteDate.pec");
		}

		[Test]
		public void testLteDateTime()
		{
			compareResourceEOE("less/lteDateTime.pec");
		}

		[Test]
		public void testLteDecimal()
		{
			compareResourceEOE("less/lteDecimal.pec");
		}

		[Test]
		public void testLteInteger()
		{
			compareResourceEOE("less/lteInteger.pec");
		}

		[Test]
		public void testLteText()
		{
			compareResourceEOE("less/lteText.pec");
		}

		[Test]
		public void testLteTime()
		{
			compareResourceEOE("less/lteTime.pec");
		}

		[Test]
		public void testLtInteger()
		{
			compareResourceEOE("less/ltInteger.pec");
		}

		[Test]
		public void testLtText()
		{
			compareResourceEOE("less/ltText.pec");
		}

		[Test]
		public void testLtTime()
		{
			compareResourceEOE("less/ltTime.pec");
		}

	}
}

