using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestGreater : BaseEParserTest
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
			CheckOutput("greater/gtCharacter.e");
		}

		[Test]
		public void testGtDate()
		{
			CheckOutput("greater/gtDate.e");
		}

		[Test]
		public void testGtDateTime()
		{
			CheckOutput("greater/gtDateTime.e");
		}

		[Test]
		public void testGtDecimal()
		{
			CheckOutput("greater/gtDecimal.e");
		}

		[Test]
		public void testGteCharacter()
		{
			CheckOutput("greater/gteCharacter.e");
		}

		[Test]
		public void testGteDate()
		{
			CheckOutput("greater/gteDate.e");
		}

		[Test]
		public void testGteDateTime()
		{
			CheckOutput("greater/gteDateTime.e");
		}

		[Test]
		public void testGteDecimal()
		{
			CheckOutput("greater/gteDecimal.e");
		}

		[Test]
		public void testGteInteger()
		{
			CheckOutput("greater/gteInteger.e");
		}

		[Test]
		public void testGteText()
		{
			CheckOutput("greater/gteText.e");
		}

		[Test]
		public void testGteTime()
		{
			CheckOutput("greater/gteTime.e");
		}

		[Test]
		public void testGtInteger()
		{
			CheckOutput("greater/gtInteger.e");
		}

		[Test]
		public void testGtText()
		{
			CheckOutput("greater/gtText.e");
		}

		[Test]
		public void testGtTime()
		{
			CheckOutput("greater/gtTime.e");
		}

	}
}

