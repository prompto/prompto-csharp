using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestMinus : BaseEParserTest
	{

		[Test]
		public void testMinusDecimal()
		{
			compareResourceEOE("minus/minusDecimal.pec");
		}

		[Test]
		public void testMinusInteger()
		{
			compareResourceEOE("minus/minusInteger.pec");
		}

		[Test]
		public void testMinusPeriod()
		{
			compareResourceEOE("minus/minusPeriod.pec");
		}

	}
}

