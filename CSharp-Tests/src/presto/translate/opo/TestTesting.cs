using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestTesting : BaseOParserTest
	{

		[Test]
		public void testNegative()
		{
			compareResourceOPO("testing/negative.o");
		}

		[Test]
		public void testNegativeError()
		{
			compareResourceOPO("testing/negativeError.o");
		}

		[Test]
		public void testPositive()
		{
			compareResourceOPO("testing/positive.o");
		}

		[Test]
		public void testPositiveError()
		{
			compareResourceOPO("testing/positiveError.o");
		}

	}
}

