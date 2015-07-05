// generated: 2015-07-05T23:01:01.368
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
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

