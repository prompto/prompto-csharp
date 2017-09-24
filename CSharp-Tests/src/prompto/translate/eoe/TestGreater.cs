using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestGreater : BaseEParserTest
	{

		[Test]
		public void testGtCharacter()
		{
			compareResourceEOE("greater/gtCharacter.pec");
		}

		[Test]
		public void testGtDate()
		{
			compareResourceEOE("greater/gtDate.pec");
		}

		[Test]
		public void testGtDateTime()
		{
			compareResourceEOE("greater/gtDateTime.pec");
		}

		[Test]
		public void testGtDecimal()
		{
			compareResourceEOE("greater/gtDecimal.pec");
		}

		[Test]
		public void testGteCharacter()
		{
			compareResourceEOE("greater/gteCharacter.pec");
		}

		[Test]
		public void testGteDate()
		{
			compareResourceEOE("greater/gteDate.pec");
		}

		[Test]
		public void testGteDateTime()
		{
			compareResourceEOE("greater/gteDateTime.pec");
		}

		[Test]
		public void testGteDecimal()
		{
			compareResourceEOE("greater/gteDecimal.pec");
		}

		[Test]
		public void testGteInteger()
		{
			compareResourceEOE("greater/gteInteger.pec");
		}

		[Test]
		public void testGteText()
		{
			compareResourceEOE("greater/gteText.pec");
		}

		[Test]
		public void testGteTime()
		{
			compareResourceEOE("greater/gteTime.pec");
		}

		[Test]
		public void testGtInteger()
		{
			compareResourceEOE("greater/gtInteger.pec");
		}

		[Test]
		public void testGtText()
		{
			compareResourceEOE("greater/gtText.pec");
		}

		[Test]
		public void testGtTime()
		{
			compareResourceEOE("greater/gtTime.pec");
		}

		[Test]
		public void testGtVersion()
		{
			compareResourceEOE("greater/gtVersion.pec");
		}

	}
}

