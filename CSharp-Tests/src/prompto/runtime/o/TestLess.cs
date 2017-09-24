using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
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
			CheckOutput("less/ltCharacter.poc");
		}

		[Test]
		public void testLtDate()
		{
			CheckOutput("less/ltDate.poc");
		}

		[Test]
		public void testLtDateTime()
		{
			CheckOutput("less/ltDateTime.poc");
		}

		[Test]
		public void testLtDecimal()
		{
			CheckOutput("less/ltDecimal.poc");
		}

		[Test]
		public void testLteCharacter()
		{
			CheckOutput("less/lteCharacter.poc");
		}

		[Test]
		public void testLteDate()
		{
			CheckOutput("less/lteDate.poc");
		}

		[Test]
		public void testLteDateTime()
		{
			CheckOutput("less/lteDateTime.poc");
		}

		[Test]
		public void testLteDecimal()
		{
			CheckOutput("less/lteDecimal.poc");
		}

		[Test]
		public void testLteInteger()
		{
			CheckOutput("less/lteInteger.poc");
		}

		[Test]
		public void testLteText()
		{
			CheckOutput("less/lteText.poc");
		}

		[Test]
		public void testLteTime()
		{
			CheckOutput("less/lteTime.poc");
		}

		[Test]
		public void testLtInteger()
		{
			CheckOutput("less/ltInteger.poc");
		}

		[Test]
		public void testLtText()
		{
			CheckOutput("less/ltText.poc");
		}

		[Test]
		public void testLtTime()
		{
			CheckOutput("less/ltTime.poc");
		}

		[Test]
		public void testLtVersion()
		{
			CheckOutput("less/ltVersion.poc");
		}

	}
}

