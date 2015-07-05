// generated: 2015-07-05T23:01:01.366
using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
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
			CheckOutput("operators/addAmount.pec");
		}

		[Test]
		public void testDivAmount()
		{
			CheckOutput("operators/divAmount.pec");
		}

		[Test]
		public void testIdivAmount()
		{
			CheckOutput("operators/idivAmount.pec");
		}

		[Test]
		public void testModAmount()
		{
			CheckOutput("operators/modAmount.pec");
		}

		[Test]
		public void testMultAmount()
		{
			CheckOutput("operators/multAmount.pec");
		}

		[Test]
		public void testSubAmount()
		{
			CheckOutput("operators/subAmount.pec");
		}

	}
}

