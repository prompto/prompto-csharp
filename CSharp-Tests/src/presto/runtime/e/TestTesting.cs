using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestTesting : BaseEParserTest
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
			CheckOutput("testing/and.e");
		}

		[Test]
		public void testContains()
		{
			CheckOutput("testing/contains.e");
		}

		[Test]
		public void testGreater()
		{
			CheckOutput("testing/greater.e");
		}

		[Test]
		public void testMethod()
		{
			CheckOutput("testing/method.e");
		}

		[Test]
		public void testNegative()
		{
			CheckOutput("testing/negative.e");
		}

		[Test]
		public void testNegativeError()
		{
			CheckOutput("testing/negativeError.e");
		}

		[Test]
		public void testNot()
		{
			CheckOutput("testing/not.e");
		}

		[Test]
		public void testOr()
		{
			CheckOutput("testing/or.e");
		}

		[Test]
		public void testPositive()
		{
			CheckOutput("testing/positive.e");
		}

		[Test]
		public void testPositiveError()
		{
			CheckOutput("testing/positiveError.e");
		}

	}
}

