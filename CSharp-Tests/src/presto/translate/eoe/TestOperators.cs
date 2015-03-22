using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestOperators : BaseEParserTest
	{

		[Test]
		public void testAddAmount()
		{
			compareResourceEOE("operators/addAmount.e");
		}

		[Test]
		public void testDivAmount()
		{
			compareResourceEOE("operators/divAmount.e");
		}

		[Test]
		public void testIdivAmount()
		{
			compareResourceEOE("operators/idivAmount.e");
		}

		[Test]
		public void testModAmount()
		{
			compareResourceEOE("operators/modAmount.e");
		}

		[Test]
		public void testMultAmount()
		{
			compareResourceEOE("operators/multAmount.e");
		}

		[Test]
		public void testSubAmount()
		{
			compareResourceEOE("operators/subAmount.e");
		}

	}
}

