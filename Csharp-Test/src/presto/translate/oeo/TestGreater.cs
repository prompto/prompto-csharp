using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestGreater : BaseOParserTest
	{

		[Test]
		public void testGtCharacter()
		{
			compareResourceOEO("greater/gtCharacter.o");
		}

		[Test]
		public void testGtDate()
		{
			compareResourceOEO("greater/gtDate.o");
		}

		[Test]
		public void testGtDateTime()
		{
			compareResourceOEO("greater/gtDateTime.o");
		}

		[Test]
		public void testGtDecimal()
		{
			compareResourceOEO("greater/gtDecimal.o");
		}

		[Test]
		public void testGteCharacter()
		{
			compareResourceOEO("greater/gteCharacter.o");
		}

		[Test]
		public void testGteDate()
		{
			compareResourceOEO("greater/gteDate.o");
		}

		[Test]
		public void testGteDateTime()
		{
			compareResourceOEO("greater/gteDateTime.o");
		}

		[Test]
		public void testGteDecimal()
		{
			compareResourceOEO("greater/gteDecimal.o");
		}

		[Test]
		public void testGteInteger()
		{
			compareResourceOEO("greater/gteInteger.o");
		}

		[Test]
		public void testGteText()
		{
			compareResourceOEO("greater/gteText.o");
		}

		[Test]
		public void testGteTime()
		{
			compareResourceOEO("greater/gteTime.o");
		}

		[Test]
		public void testGtInteger()
		{
			compareResourceOEO("greater/gtInteger.o");
		}

		[Test]
		public void testGtText()
		{
			compareResourceOEO("greater/gtText.o");
		}

		[Test]
		public void testGtTime()
		{
			compareResourceOEO("greater/gtTime.o");
		}

	}
}

