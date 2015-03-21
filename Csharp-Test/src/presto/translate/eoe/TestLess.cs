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
			compareResourceEOE("less/ltCharacter.e");
		}

		[Test]
		public void testLtDate()
		{
			compareResourceEOE("less/ltDate.e");
		}

		[Test]
		public void testLtDateTime()
		{
			compareResourceEOE("less/ltDateTime.e");
		}

		[Test]
		public void testLtDecimal()
		{
			compareResourceEOE("less/ltDecimal.e");
		}

		[Test]
		public void testLteCharacter()
		{
			compareResourceEOE("less/lteCharacter.e");
		}

		[Test]
		public void testLteDate()
		{
			compareResourceEOE("less/lteDate.e");
		}

		[Test]
		public void testLteDateTime()
		{
			compareResourceEOE("less/lteDateTime.e");
		}

		[Test]
		public void testLteDecimal()
		{
			compareResourceEOE("less/lteDecimal.e");
		}

		[Test]
		public void testLteInteger()
		{
			compareResourceEOE("less/lteInteger.e");
		}

		[Test]
		public void testLteText()
		{
			compareResourceEOE("less/lteText.e");
		}

		[Test]
		public void testLteTime()
		{
			compareResourceEOE("less/lteTime.e");
		}

		[Test]
		public void testLtInteger()
		{
			compareResourceEOE("less/ltInteger.e");
		}

		[Test]
		public void testLtText()
		{
			compareResourceEOE("less/ltText.e");
		}

		[Test]
		public void testLtTime()
		{
			compareResourceEOE("less/ltTime.e");
		}

	}
}

