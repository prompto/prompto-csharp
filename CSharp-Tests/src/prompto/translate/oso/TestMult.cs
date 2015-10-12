using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestMult : BaseOParserTest
	{

		[Test]
		public void testMultCharacter()
		{
			compareResourceOSO("mult/multCharacter.poc");
		}

		[Test]
		public void testMultDecimal()
		{
			compareResourceOSO("mult/multDecimal.poc");
		}

		[Test]
		public void testMultInteger()
		{
			compareResourceOSO("mult/multInteger.poc");
		}

		[Test]
		public void testMultList()
		{
			compareResourceOSO("mult/multList.poc");
		}

		[Test]
		public void testMultPeriod()
		{
			compareResourceOSO("mult/multPeriod.poc");
		}

		[Test]
		public void testMultText()
		{
			compareResourceOSO("mult/multText.poc");
		}

	}
}

