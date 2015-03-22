using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestLess : BaseOParserTest
	{

		[Test]
		public void testLtCharacter()
		{
			compareResourceOPO("less/ltCharacter.o");
		}

		[Test]
		public void testLtDate()
		{
			compareResourceOPO("less/ltDate.o");
		}

		[Test]
		public void testLtDateTime()
		{
			compareResourceOPO("less/ltDateTime.o");
		}

		[Test]
		public void testLtDecimal()
		{
			compareResourceOPO("less/ltDecimal.o");
		}

		[Test]
		public void testLteCharacter()
		{
			compareResourceOPO("less/lteCharacter.o");
		}

		[Test]
		public void testLteDate()
		{
			compareResourceOPO("less/lteDate.o");
		}

		[Test]
		public void testLteDateTime()
		{
			compareResourceOPO("less/lteDateTime.o");
		}

		[Test]
		public void testLteDecimal()
		{
			compareResourceOPO("less/lteDecimal.o");
		}

		[Test]
		public void testLteInteger()
		{
			compareResourceOPO("less/lteInteger.o");
		}

		[Test]
		public void testLteText()
		{
			compareResourceOPO("less/lteText.o");
		}

		[Test]
		public void testLteTime()
		{
			compareResourceOPO("less/lteTime.o");
		}

		[Test]
		public void testLtInteger()
		{
			compareResourceOPO("less/ltInteger.o");
		}

		[Test]
		public void testLtText()
		{
			compareResourceOPO("less/ltText.o");
		}

		[Test]
		public void testLtTime()
		{
			compareResourceOPO("less/ltTime.o");
		}

	}
}

