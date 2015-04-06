using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestLess : BaseOParserTest
	{

		[Test]
		public void testLtCharacter()
		{
			compareResourceOEO("less/ltCharacter.poc");
		}

		[Test]
		public void testLtDate()
		{
			compareResourceOEO("less/ltDate.poc");
		}

		[Test]
		public void testLtDateTime()
		{
			compareResourceOEO("less/ltDateTime.poc");
		}

		[Test]
		public void testLtDecimal()
		{
			compareResourceOEO("less/ltDecimal.poc");
		}

		[Test]
		public void testLteCharacter()
		{
			compareResourceOEO("less/lteCharacter.poc");
		}

		[Test]
		public void testLteDate()
		{
			compareResourceOEO("less/lteDate.poc");
		}

		[Test]
		public void testLteDateTime()
		{
			compareResourceOEO("less/lteDateTime.poc");
		}

		[Test]
		public void testLteDecimal()
		{
			compareResourceOEO("less/lteDecimal.poc");
		}

		[Test]
		public void testLteInteger()
		{
			compareResourceOEO("less/lteInteger.poc");
		}

		[Test]
		public void testLteText()
		{
			compareResourceOEO("less/lteText.poc");
		}

		[Test]
		public void testLteTime()
		{
			compareResourceOEO("less/lteTime.poc");
		}

		[Test]
		public void testLtInteger()
		{
			compareResourceOEO("less/ltInteger.poc");
		}

		[Test]
		public void testLtText()
		{
			compareResourceOEO("less/ltText.poc");
		}

		[Test]
		public void testLtTime()
		{
			compareResourceOEO("less/ltTime.poc");
		}

	}
}

