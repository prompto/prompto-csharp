using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
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
			CheckOutput("greater/gtCharacter.pec");
		}

		[Test]
		public void testGtDate()
		{
			CheckOutput("greater/gtDate.pec");
		}

		[Test]
		public void testGtDateTime()
		{
			CheckOutput("greater/gtDateTime.pec");
		}

		[Test]
		public void testGtDecimal()
		{
			CheckOutput("greater/gtDecimal.pec");
		}

		[Test]
		public void testGteCharacter()
		{
			CheckOutput("greater/gteCharacter.pec");
		}

		[Test]
		public void testGteDate()
		{
			CheckOutput("greater/gteDate.pec");
		}

		[Test]
		public void testGteDateTime()
		{
			CheckOutput("greater/gteDateTime.pec");
		}

		[Test]
		public void testGteDecimal()
		{
			CheckOutput("greater/gteDecimal.pec");
		}

		[Test]
		public void testGteInteger()
		{
			CheckOutput("greater/gteInteger.pec");
		}

		[Test]
		public void testGteText()
		{
			CheckOutput("greater/gteText.pec");
		}

		[Test]
		public void testGteTime()
		{
			CheckOutput("greater/gteTime.pec");
		}

		[Test]
		public void testGtInteger()
		{
			CheckOutput("greater/gtInteger.pec");
		}

		[Test]
		public void testGtText()
		{
			CheckOutput("greater/gtText.pec");
		}

		[Test]
		public void testGtTime()
		{
			CheckOutput("greater/gtTime.pec");
		}

		[Test]
		public void testGtVersion()
		{
			CheckOutput("greater/gtVersion.pec");
		}

	}
}

