using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestLess : BaseEParserTest
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
			CheckOutput("less/ltCharacter.e");
		}

		[Test]
		public void testLtDate()
		{
			CheckOutput("less/ltDate.e");
		}

		[Test]
		public void testLtDateTime()
		{
			CheckOutput("less/ltDateTime.e");
		}

		[Test]
		public void testLtDecimal()
		{
			CheckOutput("less/ltDecimal.e");
		}

		[Test]
		public void testLteCharacter()
		{
			CheckOutput("less/lteCharacter.e");
		}

		[Test]
		public void testLteDate()
		{
			CheckOutput("less/lteDate.e");
		}

		[Test]
		public void testLteDateTime()
		{
			CheckOutput("less/lteDateTime.e");
		}

		[Test]
		public void testLteDecimal()
		{
			CheckOutput("less/lteDecimal.e");
		}

		[Test]
		public void testLteInteger()
		{
			CheckOutput("less/lteInteger.e");
		}

		[Test]
		public void testLteText()
		{
			CheckOutput("less/lteText.e");
		}

		[Test]
		public void testLteTime()
		{
			CheckOutput("less/lteTime.e");
		}

		[Test]
		public void testLtInteger()
		{
			CheckOutput("less/ltInteger.e");
		}

		[Test]
		public void testLtText()
		{
			CheckOutput("less/ltText.e");
		}

		[Test]
		public void testLtTime()
		{
			CheckOutput("less/ltTime.e");
		}

	}
}

