using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
{

	[TestFixture]
	public class TestGreater : BaseEParserTest
	{

		[Test]
		public void testGtCharacter()
		{
			compareResourceESE("greater/gtCharacter.pec");
		}

		[Test]
		public void testGtDate()
		{
			compareResourceESE("greater/gtDate.pec");
		}

		[Test]
		public void testGtDateTime()
		{
			compareResourceESE("greater/gtDateTime.pec");
		}

		[Test]
		public void testGtDecimal()
		{
			compareResourceESE("greater/gtDecimal.pec");
		}

		[Test]
		public void testGteCharacter()
		{
			compareResourceESE("greater/gteCharacter.pec");
		}

		[Test]
		public void testGteDate()
		{
			compareResourceESE("greater/gteDate.pec");
		}

		[Test]
		public void testGteDateTime()
		{
			compareResourceESE("greater/gteDateTime.pec");
		}

		[Test]
		public void testGteDecimal()
		{
			compareResourceESE("greater/gteDecimal.pec");
		}

		[Test]
		public void testGteInteger()
		{
			compareResourceESE("greater/gteInteger.pec");
		}

		[Test]
		public void testGteText()
		{
			compareResourceESE("greater/gteText.pec");
		}

		[Test]
		public void testGteTime()
		{
			compareResourceESE("greater/gteTime.pec");
		}

		[Test]
		public void testGtInteger()
		{
			compareResourceESE("greater/gtInteger.pec");
		}

		[Test]
		public void testGtText()
		{
			compareResourceESE("greater/gtText.pec");
		}

		[Test]
		public void testGtTime()
		{
			compareResourceESE("greater/gtTime.pec");
		}

	}
}

