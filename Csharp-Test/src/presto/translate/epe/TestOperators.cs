using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestOperators : BaseEParserTest
	{

		[Test]
		public void testAddAmount()
		{
			compareResourceEPE("operators/addAmount.e");
		}

		[Test]
		public void testDivAmount()
		{
			compareResourceEPE("operators/divAmount.e");
		}

		[Test]
		public void testIdivAmount()
		{
			compareResourceEPE("operators/idivAmount.e");
		}

		[Test]
		public void testModAmount()
		{
			compareResourceEPE("operators/modAmount.e");
		}

		[Test]
		public void testMultAmount()
		{
			compareResourceEPE("operators/multAmount.e");
		}

		[Test]
		public void testSubAmount()
		{
			compareResourceEPE("operators/subAmount.e");
		}

	}
}

