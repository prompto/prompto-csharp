using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestMult : BaseOParserTest
	{

		[Test]
		public void testMultCharacter()
		{
			compareResourceOPO("mult/multCharacter.o");
		}

		[Test]
		public void testMultDecimal()
		{
			compareResourceOPO("mult/multDecimal.o");
		}

		[Test]
		public void testMultInteger()
		{
			compareResourceOPO("mult/multInteger.o");
		}

		[Test]
		public void testMultList()
		{
			compareResourceOPO("mult/multList.o");
		}

		[Test]
		public void testMultPeriod()
		{
			compareResourceOPO("mult/multPeriod.o");
		}

		[Test]
		public void testMultText()
		{
			compareResourceOPO("mult/multText.o");
		}

	}
}

