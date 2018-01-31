using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestLess : BaseEParserTest
	{

		[Test]
		public void testLtCharacter()
		{
			compareResourceEME("less/ltCharacter.pec");
		}

		[Test]
		public void testLtDate()
		{
			compareResourceEME("less/ltDate.pec");
		}

		[Test]
		public void testLtDateTime()
		{
			compareResourceEME("less/ltDateTime.pec");
		}

		[Test]
		public void testLtDecimal()
		{
			compareResourceEME("less/ltDecimal.pec");
		}

		[Test]
		public void testLtInteger()
		{
			compareResourceEME("less/ltInteger.pec");
		}

		[Test]
		public void testLtText()
		{
			compareResourceEME("less/ltText.pec");
		}

		[Test]
		public void testLtTime()
		{
			compareResourceEME("less/ltTime.pec");
		}

		[Test]
		public void testLtVersion()
		{
			compareResourceEME("less/ltVersion.pec");
		}

		[Test]
		public void testLteCharacter()
		{
			compareResourceEME("less/lteCharacter.pec");
		}

		[Test]
		public void testLteDate()
		{
			compareResourceEME("less/lteDate.pec");
		}

		[Test]
		public void testLteDateTime()
		{
			compareResourceEME("less/lteDateTime.pec");
		}

		[Test]
		public void testLteDecimal()
		{
			compareResourceEME("less/lteDecimal.pec");
		}

		[Test]
		public void testLteInteger()
		{
			compareResourceEME("less/lteInteger.pec");
		}

		[Test]
		public void testLteText()
		{
			compareResourceEME("less/lteText.pec");
		}

		[Test]
		public void testLteTime()
		{
			compareResourceEME("less/lteTime.pec");
		}

	}
}

