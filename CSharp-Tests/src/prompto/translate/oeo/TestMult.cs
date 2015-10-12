using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestMult : BaseOParserTest
	{

		[Test]
		public void testMultCharacter()
		{
			compareResourceOEO("mult/multCharacter.poc");
		}

		[Test]
		public void testMultDecimal()
		{
			compareResourceOEO("mult/multDecimal.poc");
		}

		[Test]
		public void testMultInteger()
		{
			compareResourceOEO("mult/multInteger.poc");
		}

		[Test]
		public void testMultList()
		{
			compareResourceOEO("mult/multList.poc");
		}

		[Test]
		public void testMultPeriod()
		{
			compareResourceOEO("mult/multPeriod.poc");
		}

		[Test]
		public void testMultText()
		{
			compareResourceOEO("mult/multText.poc");
		}

	}
}

