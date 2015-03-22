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
			compareResourceEOE("minus/minusDecimal.e");
		}

		[Test]
		public void testMinusInteger()
		{
			compareResourceEOE("minus/minusInteger.e");
		}

		[Test]
		public void testMinusPeriod()
		{
			compareResourceEOE("minus/minusPeriod.e");
		}

	}
}

