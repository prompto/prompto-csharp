using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestGreater : BaseOParserTest
	{

		[Test]
		public void testGtCharacter()
		{
			compareResourceOPO("greater/gtCharacter.o");
		}

		[Test]
		public void testGtDate()
		{
			compareResourceOPO("greater/gtDate.o");
		}

		[Test]
		public void testGtDateTime()
		{
			compareResourceOPO("greater/gtDateTime.o");
		}

		[Test]
		public void testGtDecimal()
		{
			compareResourceOPO("greater/gtDecimal.o");
		}

		[Test]
		public void testGteCharacter()
		{
			compareResourceOPO("greater/gteCharacter.o");
		}

		[Test]
		public void testGteDate()
		{
			compareResourceOPO("greater/gteDate.o");
		}

		[Test]
		public void testGteDateTime()
		{
			compareResourceOPO("greater/gteDateTime.o");
		}

		[Test]
		public void testGteDecimal()
		{
			compareResourceOPO("greater/gteDecimal.o");
		}

		[Test]
		public void testGteInteger()
		{
			compareResourceOPO("greater/gteInteger.o");
		}

		[Test]
		public void testGteText()
		{
			compareResourceOPO("greater/gteText.o");
		}

		[Test]
		public void testGteTime()
		{
			compareResourceOPO("greater/gteTime.o");
		}

		[Test]
		public void testGtInteger()
		{
			compareResourceOPO("greater/gtInteger.o");
		}

		[Test]
		public void testGtText()
		{
			compareResourceOPO("greater/gtText.o");
		}

		[Test]
		public void testGtTime()
		{
			compareResourceOPO("greater/gtTime.o");
		}

	}
}

