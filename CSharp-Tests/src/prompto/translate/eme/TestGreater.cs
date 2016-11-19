using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestGreater : BaseEParserTest
	{

		[Test]
		public void testGtCharacter()
		{
			compareResourceEME("greater/gtCharacter.pec");
		}

		[Test]
		public void testGtDate()
		{
			compareResourceEME("greater/gtDate.pec");
		}

		[Test]
		public void testGtDateTime()
		{
			compareResourceEME("greater/gtDateTime.pec");
		}

		[Test]
		public void testGtDecimal()
		{
			compareResourceEME("greater/gtDecimal.pec");
		}

		[Test]
		public void testGteCharacter()
		{
			compareResourceEME("greater/gteCharacter.pec");
		}

		[Test]
		public void testGteDate()
		{
			compareResourceEME("greater/gteDate.pec");
		}

		[Test]
		public void testGteDateTime()
		{
			compareResourceEME("greater/gteDateTime.pec");
		}

		[Test]
		public void testGteDecimal()
		{
			compareResourceEME("greater/gteDecimal.pec");
		}

		[Test]
		public void testGteInteger()
		{
			compareResourceEME("greater/gteInteger.pec");
		}

		[Test]
		public void testGteText()
		{
			compareResourceEME("greater/gteText.pec");
		}

		[Test]
		public void testGteTime()
		{
			compareResourceEME("greater/gteTime.pec");
		}

		[Test]
		public void testGtInteger()
		{
			compareResourceEME("greater/gtInteger.pec");
		}

		[Test]
		public void testGtText()
		{
			compareResourceEME("greater/gtText.pec");
		}

		[Test]
		public void testGtTime()
		{
			compareResourceEME("greater/gtTime.pec");
		}

	}
}

