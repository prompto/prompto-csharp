using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestLess : BaseOParserTest
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
		public void testLtCharacter()
		{
			CheckOutput("less/ltCharacter.o");
		}

		[Test]
		public void testLtDate()
		{
			CheckOutput("less/ltDate.o");
		}

		[Test]
		public void testLtDateTime()
		{
			CheckOutput("less/ltDateTime.o");
		}

		[Test]
		public void testLtDecimal()
		{
			CheckOutput("less/ltDecimal.o");
		}

		[Test]
		public void testLteCharacter()
		{
			CheckOutput("less/lteCharacter.o");
		}

		[Test]
		public void testLteDate()
		{
			CheckOutput("less/lteDate.o");
		}

		[Test]
		public void testLteDateTime()
		{
			CheckOutput("less/lteDateTime.o");
		}

		[Test]
		public void testLteDecimal()
		{
			CheckOutput("less/lteDecimal.o");
		}

		[Test]
		public void testLteInteger()
		{
			CheckOutput("less/lteInteger.o");
		}

		[Test]
		public void testLteText()
		{
			CheckOutput("less/lteText.o");
		}

		[Test]
		public void testLteTime()
		{
			CheckOutput("less/lteTime.o");
		}

		[Test]
		public void testLtInteger()
		{
			CheckOutput("less/ltInteger.o");
		}

		[Test]
		public void testLtText()
		{
			CheckOutput("less/ltText.o");
		}

		[Test]
		public void testLtTime()
		{
			CheckOutput("less/ltTime.o");
		}

	}
}

