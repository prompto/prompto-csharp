using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestMult : BaseEParserTest
	{

		[Test]
		public void testMultCharacter()
		{
			compareResourceEPE("mult/multCharacter.e");
		}

		[Test]
		public void testMultDecimal()
		{
			compareResourceEPE("mult/multDecimal.e");
		}

		[Test]
		public void testMultInteger()
		{
			compareResourceEPE("mult/multInteger.e");
		}

		[Test]
		public void testMultList()
		{
			compareResourceEPE("mult/multList.e");
		}

		[Test]
		public void testMultPeriod()
		{
			compareResourceEPE("mult/multPeriod.e");
		}

		[Test]
		public void testMultText()
		{
			compareResourceEPE("mult/multText.e");
		}

	}
}

