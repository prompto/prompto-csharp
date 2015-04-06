using NUnit.Framework;
using presto.parser;

namespace presto.translate.oso
{

	[TestFixture]
	public class TestOperators : BaseOParserTest
	{

		[Test]
		public void testAddAmount()
		{
			compareResourceOSO("operators/addAmount.poc");
		}

		[Test]
		public void testDivAmount()
		{
			compareResourceOSO("operators/divAmount.poc");
		}

		[Test]
		public void testIdivAmount()
		{
			compareResourceOSO("operators/idivAmount.poc");
		}

		[Test]
		public void testModAmount()
		{
			compareResourceOSO("operators/modAmount.poc");
		}

		[Test]
		public void testMultAmount()
		{
			compareResourceOSO("operators/multAmount.poc");
		}

		[Test]
		public void testSubAmount()
		{
			compareResourceOSO("operators/subAmount.poc");
		}

	}
}

