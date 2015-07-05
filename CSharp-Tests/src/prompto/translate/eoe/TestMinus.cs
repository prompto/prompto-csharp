// generated: 2015-07-05T23:01:01.332
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
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

