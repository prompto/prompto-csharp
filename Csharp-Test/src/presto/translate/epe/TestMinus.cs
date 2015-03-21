using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestMinus : BaseEParserTest
	{

		[Test]
		public void testMinusDecimal()
		{
			compareResourceEPE("minus/minusDecimal.e");
		}

		[Test]
		public void testMinusInteger()
		{
			compareResourceEPE("minus/minusInteger.e");
		}

		[Test]
		public void testMinusPeriod()
		{
			compareResourceEPE("minus/minusPeriod.e");
		}

	}
}

