using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestOperators : BaseOParserTest
	{

		[Test]
		public void testAddAmount()
		{
			compareResourceOMO("operators/addAmount.poc");
		}

		[Test]
		public void testDivAmount()
		{
			compareResourceOMO("operators/divAmount.poc");
		}

		[Test]
		public void testIdivAmount()
		{
			compareResourceOMO("operators/idivAmount.poc");
		}

		[Test]
		public void testModAmount()
		{
			compareResourceOMO("operators/modAmount.poc");
		}

		[Test]
		public void testMultAmount()
		{
			compareResourceOMO("operators/multAmount.poc");
		}

		[Test]
		public void testSubAmount()
		{
			compareResourceOMO("operators/subAmount.poc");
		}

	}
}

