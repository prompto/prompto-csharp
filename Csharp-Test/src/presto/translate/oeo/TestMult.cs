using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestMult : BaseOParserTest
	{

		[Test]
		public void testMultCharacter()
		{
			compareResourceOEO("mult/multCharacter.o");
		}

		[Test]
		public void testMultDecimal()
		{
			compareResourceOEO("mult/multDecimal.o");
		}

		[Test]
		public void testMultInteger()
		{
			compareResourceOEO("mult/multInteger.o");
		}

		[Test]
		public void testMultList()
		{
			compareResourceOEO("mult/multList.o");
		}

		[Test]
		public void testMultPeriod()
		{
			compareResourceOEO("mult/multPeriod.o");
		}

		[Test]
		public void testMultText()
		{
			compareResourceOEO("mult/multText.o");
		}

	}
}

