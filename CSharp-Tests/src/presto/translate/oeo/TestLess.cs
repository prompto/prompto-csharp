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
			compareResourceOEO("less/ltCharacter.o");
		}

		[Test]
		public void testLtDate()
		{
			compareResourceOEO("less/ltDate.o");
		}

		[Test]
		public void testLtDateTime()
		{
			compareResourceOEO("less/ltDateTime.o");
		}

		[Test]
		public void testLtDecimal()
		{
			compareResourceOEO("less/ltDecimal.o");
		}

		[Test]
		public void testLteCharacter()
		{
			compareResourceOEO("less/lteCharacter.o");
		}

		[Test]
		public void testLteDate()
		{
			compareResourceOEO("less/lteDate.o");
		}

		[Test]
		public void testLteDateTime()
		{
			compareResourceOEO("less/lteDateTime.o");
		}

		[Test]
		public void testLteDecimal()
		{
			compareResourceOEO("less/lteDecimal.o");
		}

		[Test]
		public void testLteInteger()
		{
			compareResourceOEO("less/lteInteger.o");
		}

		[Test]
		public void testLteText()
		{
			compareResourceOEO("less/lteText.o");
		}

		[Test]
		public void testLteTime()
		{
			compareResourceOEO("less/lteTime.o");
		}

		[Test]
		public void testLtInteger()
		{
			compareResourceOEO("less/ltInteger.o");
		}

		[Test]
		public void testLtText()
		{
			compareResourceOEO("less/ltText.o");
		}

		[Test]
		public void testLtTime()
		{
			compareResourceOEO("less/ltTime.o");
		}

	}
}

