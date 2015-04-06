using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
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

