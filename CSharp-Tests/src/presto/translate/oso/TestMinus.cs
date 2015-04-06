using NUnit.Framework;
using presto.parser;

namespace presto.translate.oso
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

