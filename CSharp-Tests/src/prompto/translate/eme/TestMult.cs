using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestMult : BaseEParserTest
	{

		[Test]
		public void testMultCharacter()
		{
			compareResourceEME("mult/multCharacter.pec");
		}

		[Test]
		public void testMultDecimal()
		{
			compareResourceEME("mult/multDecimal.pec");
		}

		[Test]
		public void testMultInteger()
		{
			compareResourceEME("mult/multInteger.pec");
		}

		[Test]
		public void testMultList()
		{
			compareResourceEME("mult/multList.pec");
		}

		[Test]
		public void testMultPeriod()
		{
			compareResourceEME("mult/multPeriod.pec");
		}

		[Test]
		public void testMultText()
		{
			compareResourceEME("mult/multText.pec");
		}

	}
}

