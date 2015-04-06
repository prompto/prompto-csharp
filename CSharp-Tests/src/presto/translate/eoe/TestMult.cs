using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestMult : BaseEParserTest
	{

		[Test]
		public void testMultCharacter()
		{
			compareResourceEOE("mult/multCharacter.pec");
		}

		[Test]
		public void testMultDecimal()
		{
			compareResourceEOE("mult/multDecimal.pec");
		}

		[Test]
		public void testMultInteger()
		{
			compareResourceEOE("mult/multInteger.pec");
		}

		[Test]
		public void testMultList()
		{
			compareResourceEOE("mult/multList.pec");
		}

		[Test]
		public void testMultPeriod()
		{
			compareResourceEOE("mult/multPeriod.pec");
		}

		[Test]
		public void testMultText()
		{
			compareResourceEOE("mult/multText.pec");
		}

	}
}

