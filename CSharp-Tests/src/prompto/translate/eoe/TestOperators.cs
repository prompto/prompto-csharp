using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestOperators : BaseEParserTest
	{

		[Test]
		public void testAddAmount()
		{
			compareResourceEOE("operators/addAmount.pec");
		}

		[Test]
		public void testDivAmount()
		{
			compareResourceEOE("operators/divAmount.pec");
		}

		[Test]
		public void testIdivAmount()
		{
			compareResourceEOE("operators/idivAmount.pec");
		}

		[Test]
		public void testModAmount()
		{
			compareResourceEOE("operators/modAmount.pec");
		}

		[Test]
		public void testMultAmount()
		{
			compareResourceEOE("operators/multAmount.pec");
		}

		[Test]
		public void testSubAmount()
		{
			compareResourceEOE("operators/subAmount.pec");
		}

	}
}

