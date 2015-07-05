// generated: 2015-07-05T23:01:01.367
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestOperators : BaseOParserTest
	{

		[Test]
		public void testAddAmount()
		{
			compareResourceOEO("operators/addAmount.poc");
		}

		[Test]
		public void testDivAmount()
		{
			compareResourceOEO("operators/divAmount.poc");
		}

		[Test]
		public void testIdivAmount()
		{
			compareResourceOEO("operators/idivAmount.poc");
		}

		[Test]
		public void testModAmount()
		{
			compareResourceOEO("operators/modAmount.poc");
		}

		[Test]
		public void testMultAmount()
		{
			compareResourceOEO("operators/multAmount.poc");
		}

		[Test]
		public void testSubAmount()
		{
			compareResourceOEO("operators/subAmount.poc");
		}

	}
}

