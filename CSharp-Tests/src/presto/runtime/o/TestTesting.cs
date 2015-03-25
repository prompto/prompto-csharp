using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestTesting : BaseOParserTest
	{

		[SetUp]
		public void before()
		{
			Out.init();
		}

		[TearDown]
		public void after()
		{
			Out.restore();
		}

		[Test]
		public void testNegative()
		{
			CheckOutput("testing/negative.o");
		}

		[Test]
		public void testNegativeError()
		{
			CheckOutput("testing/negativeError.o");
		}

		[Test]
		public void testPositive()
		{
			CheckOutput("testing/positive.o");
		}

		[Test]
		public void testPositiveError()
		{
			CheckOutput("testing/positiveError.o");
		}

	}
}

