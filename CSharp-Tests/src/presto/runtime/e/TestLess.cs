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
			CheckOutput("less/ltCharacter.pec");
		}

		[Test]
		public void testLtDate()
		{
			CheckOutput("less/ltDate.pec");
		}

		[Test]
		public void testLtDateTime()
		{
			CheckOutput("less/ltDateTime.pec");
		}

		[Test]
		public void testLtDecimal()
		{
			CheckOutput("less/ltDecimal.pec");
		}

		[Test]
		public void testLteCharacter()
		{
			CheckOutput("less/lteCharacter.pec");
		}

		[Test]
		public void testLteDate()
		{
			CheckOutput("less/lteDate.pec");
		}

		[Test]
		public void testLteDateTime()
		{
			CheckOutput("less/lteDateTime.pec");
		}

		[Test]
		public void testLteDecimal()
		{
			CheckOutput("less/lteDecimal.pec");
		}

		[Test]
		public void testLteInteger()
		{
			CheckOutput("less/lteInteger.pec");
		}

		[Test]
		public void testLteText()
		{
			CheckOutput("less/lteText.pec");
		}

		[Test]
		public void testLteTime()
		{
			CheckOutput("less/lteTime.pec");
		}

		[Test]
		public void testLtInteger()
		{
			CheckOutput("less/ltInteger.pec");
		}

		[Test]
		public void testLtText()
		{
			CheckOutput("less/ltText.pec");
		}

		[Test]
		public void testLtTime()
		{
			CheckOutput("less/ltTime.pec");
		}

	}
}

