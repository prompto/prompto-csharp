using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestDiv : BaseOParserTest
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
			CheckOutput("div/divDecimal.o");
		}

		[Test]
		public void testDivInteger()
		{
			CheckOutput("div/divInteger.o");
		}

		[Test]
		public void testIdivInteger()
		{
			CheckOutput("div/idivInteger.o");
		}

		[Test]
		public void testModInteger()
		{
			CheckOutput("div/modInteger.o");
		}

	}
}

