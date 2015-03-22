using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestGreater : BaseEParserTest
	{

		[Test]
		public void testGtCharacter()
		{
			compareResourceEPE("greater/gtCharacter.e");
		}

		[Test]
		public void testGtDate()
		{
			compareResourceEPE("greater/gtDate.e");
		}

		[Test]
		public void testGtDateTime()
		{
			compareResourceEPE("greater/gtDateTime.e");
		}

		[Test]
		public void testGtDecimal()
		{
			compareResourceEPE("greater/gtDecimal.e");
		}

		[Test]
		public void testGteCharacter()
		{
			compareResourceEPE("greater/gteCharacter.e");
		}

		[Test]
		public void testGteDate()
		{
			compareResourceEPE("greater/gteDate.e");
		}

		[Test]
		public void testGteDateTime()
		{
			compareResourceEPE("greater/gteDateTime.e");
		}

		[Test]
		public void testGteDecimal()
		{
			compareResourceEPE("greater/gteDecimal.e");
		}

		[Test]
		public void testGteInteger()
		{
			compareResourceEPE("greater/gteInteger.e");
		}

		[Test]
		public void testGteText()
		{
			compareResourceEPE("greater/gteText.e");
		}

		[Test]
		public void testGteTime()
		{
			compareResourceEPE("greater/gteTime.e");
		}

		[Test]
		public void testGtInteger()
		{
			compareResourceEPE("greater/gtInteger.e");
		}

		[Test]
		public void testGtText()
		{
			compareResourceEPE("greater/gtText.e");
		}

		[Test]
		public void testGtTime()
		{
			compareResourceEPE("greater/gtTime.e");
		}

	}
}

