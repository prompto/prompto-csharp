using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestGreater : BaseEParserTest
	{

		[Test]
		public void testGtCharacter()
		{
			compareResourceEOE("greater/gtCharacter.e");
		}

		[Test]
		public void testGtDate()
		{
			compareResourceEOE("greater/gtDate.e");
		}

		[Test]
		public void testGtDateTime()
		{
			compareResourceEOE("greater/gtDateTime.e");
		}

		[Test]
		public void testGtDecimal()
		{
			compareResourceEOE("greater/gtDecimal.e");
		}

		[Test]
		public void testGteCharacter()
		{
			compareResourceEOE("greater/gteCharacter.e");
		}

		[Test]
		public void testGteDate()
		{
			compareResourceEOE("greater/gteDate.e");
		}

		[Test]
		public void testGteDateTime()
		{
			compareResourceEOE("greater/gteDateTime.e");
		}

		[Test]
		public void testGteDecimal()
		{
			compareResourceEOE("greater/gteDecimal.e");
		}

		[Test]
		public void testGteInteger()
		{
			compareResourceEOE("greater/gteInteger.e");
		}

		[Test]
		public void testGteText()
		{
			compareResourceEOE("greater/gteText.e");
		}

		[Test]
		public void testGteTime()
		{
			compareResourceEOE("greater/gteTime.e");
		}

		[Test]
		public void testGtInteger()
		{
			compareResourceEOE("greater/gtInteger.e");
		}

		[Test]
		public void testGtText()
		{
			compareResourceEOE("greater/gtText.e");
		}

		[Test]
		public void testGtTime()
		{
			compareResourceEOE("greater/gtTime.e");
		}

	}
}

