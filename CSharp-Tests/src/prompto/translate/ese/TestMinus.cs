using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestMinus : BaseEParserTest
	{

		[Test]
		public void testMinusDecimal()
		{
			compareResourceESE("minus/minusDecimal.pec");
		}

		[Test]
		public void testMinusInteger()
		{
			compareResourceESE("minus/minusInteger.pec");
		}

		[Test]
		public void testMinusPeriod()
		{
			compareResourceESE("minus/minusPeriod.pec");
		}

	}
}

