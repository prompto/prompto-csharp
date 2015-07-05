// generated: 2015-07-05T23:01:01.336
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestMinus : BaseOParserTest
	{

		[Test]
		public void testMinusDecimal()
		{
			compareResourceOSO("minus/minusDecimal.poc");
		}

		[Test]
		public void testMinusInteger()
		{
			compareResourceOSO("minus/minusInteger.poc");
		}

		[Test]
		public void testMinusPeriod()
		{
			compareResourceOSO("minus/minusPeriod.poc");
		}

	}
}

