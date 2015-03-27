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
		public void testAnd()
		{
			CheckOutput("testing/and.o");
		}

		[Test]
		public void testContains()
		{
			CheckOutput("testing/contains.o");
		}

		[Test]
		public void testGreater()
		{
			CheckOutput("testing/greater.o");
		}

		[Test]
		public void testMethod()
		{
			CheckOutput("testing/method.o");
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
		public void testNot()
		{
			CheckOutput("testing/not.o");
		}

		[Test]
		public void testOr()
		{
			CheckOutput("testing/or.o");
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

