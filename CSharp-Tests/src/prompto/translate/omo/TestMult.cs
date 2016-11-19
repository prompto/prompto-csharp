using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestMult : BaseOParserTest
	{

		[Test]
		public void testMultCharacter()
		{
			compareResourceOMO("mult/multCharacter.poc");
		}

		[Test]
		public void testMultDecimal()
		{
			compareResourceOMO("mult/multDecimal.poc");
		}

		[Test]
		public void testMultInteger()
		{
			compareResourceOMO("mult/multInteger.poc");
		}

		[Test]
		public void testMultList()
		{
			compareResourceOMO("mult/multList.poc");
		}

		[Test]
		public void testMultPeriod()
		{
			compareResourceOMO("mult/multPeriod.poc");
		}

		[Test]
		public void testMultText()
		{
			compareResourceOMO("mult/multText.poc");
		}

	}
}

