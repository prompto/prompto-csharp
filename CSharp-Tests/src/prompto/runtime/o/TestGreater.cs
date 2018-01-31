using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
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
			CheckOutput("greater/gtCharacter.poc");
		}

		[Test]
		public void testGtDate()
		{
			CheckOutput("greater/gtDate.poc");
		}

		[Test]
		public void testGtDateTime()
		{
			CheckOutput("greater/gtDateTime.poc");
		}

		[Test]
		public void testGtDecimal()
		{
			CheckOutput("greater/gtDecimal.poc");
		}

		[Test]
		public void testGtInteger()
		{
			CheckOutput("greater/gtInteger.poc");
		}

		[Test]
		public void testGtText()
		{
			CheckOutput("greater/gtText.poc");
		}

		[Test]
		public void testGtTime()
		{
			CheckOutput("greater/gtTime.poc");
		}

		[Test]
		public void testGtVersion()
		{
			CheckOutput("greater/gtVersion.poc");
		}

		[Test]
		public void testGteCharacter()
		{
			CheckOutput("greater/gteCharacter.poc");
		}

		[Test]
		public void testGteDate()
		{
			CheckOutput("greater/gteDate.poc");
		}

		[Test]
		public void testGteDateTime()
		{
			CheckOutput("greater/gteDateTime.poc");
		}

		[Test]
		public void testGteDecimal()
		{
			CheckOutput("greater/gteDecimal.poc");
		}

		[Test]
		public void testGteInteger()
		{
			CheckOutput("greater/gteInteger.poc");
		}

		[Test]
		public void testGteText()
		{
			CheckOutput("greater/gteText.poc");
		}

		[Test]
		public void testGteTime()
		{
			CheckOutput("greater/gteTime.poc");
		}

	}
}

