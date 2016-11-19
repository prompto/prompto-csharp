using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestLess : BaseOParserTest
	{

		[Test]
		public void testLtCharacter()
		{
			compareResourceOMO("less/ltCharacter.poc");
		}

		[Test]
		public void testLtDate()
		{
			compareResourceOMO("less/ltDate.poc");
		}

		[Test]
		public void testLtDateTime()
		{
			compareResourceOMO("less/ltDateTime.poc");
		}

		[Test]
		public void testLtDecimal()
		{
			compareResourceOMO("less/ltDecimal.poc");
		}

		[Test]
		public void testLteCharacter()
		{
			compareResourceOMO("less/lteCharacter.poc");
		}

		[Test]
		public void testLteDate()
		{
			compareResourceOMO("less/lteDate.poc");
		}

		[Test]
		public void testLteDateTime()
		{
			compareResourceOMO("less/lteDateTime.poc");
		}

		[Test]
		public void testLteDecimal()
		{
			compareResourceOMO("less/lteDecimal.poc");
		}

		[Test]
		public void testLteInteger()
		{
			compareResourceOMO("less/lteInteger.poc");
		}

		[Test]
		public void testLteText()
		{
			compareResourceOMO("less/lteText.poc");
		}

		[Test]
		public void testLteTime()
		{
			compareResourceOMO("less/lteTime.poc");
		}

		[Test]
		public void testLtInteger()
		{
			compareResourceOMO("less/ltInteger.poc");
		}

		[Test]
		public void testLtText()
		{
			compareResourceOMO("less/ltText.poc");
		}

		[Test]
		public void testLtTime()
		{
			compareResourceOMO("less/ltTime.poc");
		}

	}
}

