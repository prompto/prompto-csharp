using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestTesting : BaseOParserTest
	{

		[Test]
		public void testNegative()
		{
			compareResourceOEO("testing/negative.o");
		}

		[Test]
		public void testNegativeError()
		{
			compareResourceOEO("testing/negativeError.o");
		}

		[Test]
		public void testPositive()
		{
			compareResourceOEO("testing/positive.o");
		}

		[Test]
		public void testPositiveError()
		{
			compareResourceOEO("testing/positiveError.o");
		}

	}
}

