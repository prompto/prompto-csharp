// generated: 2015-07-05T23:01:01.335
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestMinus : BaseOParserTest
	{

		[Test]
		public void testMinusDecimal()
		{
			compareResourceOEO("minus/minusDecimal.poc");
		}

		[Test]
		public void testMinusInteger()
		{
			compareResourceOEO("minus/minusInteger.poc");
		}

		[Test]
		public void testMinusPeriod()
		{
			compareResourceOEO("minus/minusPeriod.poc");
		}

	}
}

