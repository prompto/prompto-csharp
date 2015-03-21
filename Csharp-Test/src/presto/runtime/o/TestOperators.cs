using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestOperators : BaseOParserTest
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
		public void testAddAmount()
		{
			CheckOutput("operators/addAmount.o");
		}

		[Test]
		public void testDivAmount()
		{
			CheckOutput("operators/divAmount.o");
		}

		[Test]
		public void testIdivAmount()
		{
			CheckOutput("operators/idivAmount.o");
		}

		[Test]
		public void testModAmount()
		{
			CheckOutput("operators/modAmount.o");
		}

		[Test]
		public void testMultAmount()
		{
			CheckOutput("operators/multAmount.o");
		}

		[Test]
		public void testSubAmount()
		{
			CheckOutput("operators/subAmount.o");
		}

	}
}

