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
			compareResourceEOE("mult/multCharacter.e");
		}

		[Test]
		public void testMultDecimal()
		{
			compareResourceEOE("mult/multDecimal.e");
		}

		[Test]
		public void testMultInteger()
		{
			compareResourceEOE("mult/multInteger.e");
		}

		[Test]
		public void testMultList()
		{
			compareResourceEOE("mult/multList.e");
		}

		[Test]
		public void testMultPeriod()
		{
			compareResourceEOE("mult/multPeriod.e");
		}

		[Test]
		public void testMultText()
		{
			compareResourceEOE("mult/multText.e");
		}

	}
}

