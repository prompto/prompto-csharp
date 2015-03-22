using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestOperators : BaseOParserTest
	{

		[Test]
		public void testAddAmount()
		{
			compareResourceOPO("operators/addAmount.o");
		}

		[Test]
		public void testDivAmount()
		{
			compareResourceOPO("operators/divAmount.o");
		}

		[Test]
		public void testIdivAmount()
		{
			compareResourceOPO("operators/idivAmount.o");
		}

		[Test]
		public void testModAmount()
		{
			compareResourceOPO("operators/modAmount.o");
		}

		[Test]
		public void testMultAmount()
		{
			compareResourceOPO("operators/multAmount.o");
		}

		[Test]
		public void testSubAmount()
		{
			compareResourceOPO("operators/subAmount.o");
		}

	}
}

