// generated: 2015-07-05T23:01:01.333
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

