using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestOperators : BaseEParserTest
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
			CheckOutput("operators/addAmount.e");
		}

		[Test]
		public void testDivAmount()
		{
			CheckOutput("operators/divAmount.e");
		}

		[Test]
		public void testIdivAmount()
		{
			CheckOutput("operators/idivAmount.e");
		}

		[Test]
		public void testModAmount()
		{
			CheckOutput("operators/modAmount.e");
		}

		[Test]
		public void testMultAmount()
		{
			CheckOutput("operators/multAmount.e");
		}

		[Test]
		public void testSubAmount()
		{
			CheckOutput("operators/subAmount.e");
		}

	}
}

