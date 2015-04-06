using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
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

