using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestMinus : BaseOParserTest
	{

		[Test]
		public void testMinusDecimal()
		{
			compareResourceOPO("minus/minusDecimal.o");
		}

		[Test]
		public void testMinusInteger()
		{
			compareResourceOPO("minus/minusInteger.o");
		}

		[Test]
		public void testMinusPeriod()
		{
			compareResourceOPO("minus/minusPeriod.o");
		}

	}
}

