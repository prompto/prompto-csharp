using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestLess : BaseEParserTest
	{

		[Test]
		public void testLtCharacter()
		{
			compareResourceESE("less/ltCharacter.pec");
		}

		[Test]
		public void testLtDate()
		{
			compareResourceESE("less/ltDate.pec");
		}

		[Test]
		public void testLtDateTime()
		{
			compareResourceESE("less/ltDateTime.pec");
		}

		[Test]
		public void testLtDecimal()
		{
			compareResourceESE("less/ltDecimal.pec");
		}

		[Test]
		public void testLteCharacter()
		{
			compareResourceESE("less/lteCharacter.pec");
		}

		[Test]
		public void testLteDate()
		{
			compareResourceESE("less/lteDate.pec");
		}

		[Test]
		public void testLteDateTime()
		{
			compareResourceESE("less/lteDateTime.pec");
		}

		[Test]
		public void testLteDecimal()
		{
			compareResourceESE("less/lteDecimal.pec");
		}

		[Test]
		public void testLteInteger()
		{
			compareResourceESE("less/lteInteger.pec");
		}

		[Test]
		public void testLteText()
		{
			compareResourceESE("less/lteText.pec");
		}

		[Test]
		public void testLteTime()
		{
			compareResourceESE("less/lteTime.pec");
		}

		[Test]
		public void testLtInteger()
		{
			compareResourceESE("less/ltInteger.pec");
		}

		[Test]
		public void testLtText()
		{
			compareResourceESE("less/ltText.pec");
		}

		[Test]
		public void testLtTime()
		{
			compareResourceESE("less/ltTime.pec");
		}

	}
}

