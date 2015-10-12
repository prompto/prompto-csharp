using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
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
			CheckOutput("operators/addAmount.poc");
		}

		[Test]
		public void testDivAmount()
		{
			CheckOutput("operators/divAmount.poc");
		}

		[Test]
		public void testIdivAmount()
		{
			CheckOutput("operators/idivAmount.poc");
		}

		[Test]
		public void testModAmount()
		{
			CheckOutput("operators/modAmount.poc");
		}

		[Test]
		public void testMultAmount()
		{
			CheckOutput("operators/multAmount.poc");
		}

		[Test]
		public void testSubAmount()
		{
			CheckOutput("operators/subAmount.poc");
		}

	}
}

