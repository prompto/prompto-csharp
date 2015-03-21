using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestGreater : BaseOParserTest
	{

		[SetUp]
		public void before()
		{
			Out.init();
		}

		[TearDown]
		public void after()
		{
			Out.restore();
		}

		[Test]
		public void testGtCharacter()
		{
			CheckOutput("greater/gtCharacter.o");
		}

		[Test]
		public void testGtDate()
		{
			CheckOutput("greater/gtDate.o");
		}

		[Test]
		public void testGtDateTime()
		{
			CheckOutput("greater/gtDateTime.o");
		}

		[Test]
		public void testGtDecimal()
		{
			CheckOutput("greater/gtDecimal.o");
		}

		[Test]
		public void testGteCharacter()
		{
			CheckOutput("greater/gteCharacter.o");
		}

		[Test]
		public void testGteDate()
		{
			CheckOutput("greater/gteDate.o");
		}

		[Test]
		public void testGteDateTime()
		{
			CheckOutput("greater/gteDateTime.o");
		}

		[Test]
		public void testGteDecimal()
		{
			CheckOutput("greater/gteDecimal.o");
		}

		[Test]
		public void testGteInteger()
		{
			CheckOutput("greater/gteInteger.o");
		}

		[Test]
		public void testGteText()
		{
			CheckOutput("greater/gteText.o");
		}

		[Test]
		public void testGteTime()
		{
			CheckOutput("greater/gteTime.o");
		}

		[Test]
		public void testGtInteger()
		{
			CheckOutput("greater/gtInteger.o");
		}

		[Test]
		public void testGtText()
		{
			CheckOutput("greater/gtText.o");
		}

		[Test]
		public void testGtTime()
		{
			CheckOutput("greater/gtTime.o");
		}

	}
}

