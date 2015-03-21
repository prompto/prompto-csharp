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
			CheckOutput("div/divDecimal.e");
		}

		[Test]
		public void testDivInteger()
		{
			CheckOutput("div/divInteger.e");
		}

		[Test]
		public void testIdivInteger()
		{
			CheckOutput("div/idivInteger.e");
		}

		[Test]
		public void testModInteger()
		{
			CheckOutput("div/modInteger.e");
		}

	}
}

