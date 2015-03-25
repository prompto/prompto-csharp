using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestTesting : BaseEParserTest
	{

		[Test]
		public void testNegative()
		{
			compareResourceEPE("testing/negative.e");
		}

		[Test]
		public void testNegativeError()
		{
			compareResourceEPE("testing/negativeError.e");
		}

		[Test]
		public void testPositive()
		{
			compareResourceEPE("testing/positive.e");
		}

		[Test]
		public void testPositiveError()
		{
			compareResourceEPE("testing/positiveError.e");
		}

	}
}

