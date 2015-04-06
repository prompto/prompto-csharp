using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestDiv : BaseEParserTest
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
		public void testDivDecimal()
		{
			CheckOutput("div/divDecimal.pec");
		}

		[Test]
		public void testDivInteger()
		{
			CheckOutput("div/divInteger.pec");
		}

		[Test]
		public void testIdivInteger()
		{
			CheckOutput("div/idivInteger.pec");
		}

		[Test]
		public void testModInteger()
		{
			CheckOutput("div/modInteger.pec");
		}

	}
}

