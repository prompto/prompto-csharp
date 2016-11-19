using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestOperators : BaseEParserTest
	{

		[Test]
		public void testAddAmount()
		{
			compareResourceEME("operators/addAmount.pec");
		}

		[Test]
		public void testDivAmount()
		{
			compareResourceEME("operators/divAmount.pec");
		}

		[Test]
		public void testIdivAmount()
		{
			compareResourceEME("operators/idivAmount.pec");
		}

		[Test]
		public void testModAmount()
		{
			compareResourceEME("operators/modAmount.pec");
		}

		[Test]
		public void testMultAmount()
		{
			compareResourceEME("operators/multAmount.pec");
		}

		[Test]
		public void testSubAmount()
		{
			compareResourceEME("operators/subAmount.pec");
		}

	}
}

