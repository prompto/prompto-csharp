using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestOperators : BaseOParserTest
	{

		[Test]
		public void testAddAmount()
		{
			compareResourceOEO("operators/addAmount.o");
		}

		[Test]
		public void testDivAmount()
		{
			compareResourceOEO("operators/divAmount.o");
		}

		[Test]
		public void testIdivAmount()
		{
			compareResourceOEO("operators/idivAmount.o");
		}

		[Test]
		public void testModAmount()
		{
			compareResourceOEO("operators/modAmount.o");
		}

		[Test]
		public void testMultAmount()
		{
			compareResourceOEO("operators/multAmount.o");
		}

		[Test]
		public void testSubAmount()
		{
			compareResourceOEO("operators/subAmount.o");
		}

	}
}

