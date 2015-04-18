using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
{

	[TestFixture]
	public class TestMult : BaseEParserTest
	{

		[Test]
		public void testMultCharacter()
		{
			compareResourceESE("mult/multCharacter.pec");
		}

		[Test]
		public void testMultDecimal()
		{
			compareResourceESE("mult/multDecimal.pec");
		}

		[Test]
		public void testMultInteger()
		{
			compareResourceESE("mult/multInteger.pec");
		}

		[Test]
		public void testMultList()
		{
			compareResourceESE("mult/multList.pec");
		}

		[Test]
		public void testMultPeriod()
		{
			compareResourceESE("mult/multPeriod.pec");
		}

		[Test]
		public void testMultText()
		{
			compareResourceESE("mult/multText.pec");
		}

	}
}
