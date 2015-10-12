using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestOperators : BaseEParserTest
	{

		[Test]
		public void testAddAmount()
		{
			compareResourceESE("operators/addAmount.pec");
		}

		[Test]
		public void testDivAmount()
		{
			compareResourceESE("operators/divAmount.pec");
		}

		[Test]
		public void testIdivAmount()
		{
			compareResourceESE("operators/idivAmount.pec");
		}

		[Test]
		public void testModAmount()
		{
			compareResourceESE("operators/modAmount.pec");
		}

		[Test]
		public void testMultAmount()
		{
			compareResourceESE("operators/multAmount.pec");
		}

		[Test]
		public void testSubAmount()
		{
			compareResourceESE("operators/subAmount.pec");
		}

	}
}

