using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestTesting : BaseEParserTest
	{

		[Test]
		public void testNegative()
		{
			compareResourceEOE("testing/negative.e");
		}

		[Test]
		public void testNegativeError()
		{
			compareResourceEOE("testing/negativeError.e");
		}

		[Test]
		public void testPositive()
		{
			compareResourceEOE("testing/positive.e");
		}

		[Test]
		public void testPositiveError()
		{
			compareResourceEOE("testing/positiveError.e");
		}

	}
}

