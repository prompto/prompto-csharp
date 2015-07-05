// generated: 2015-07-05T23:01:01.441
using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
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
			CheckOutput("testing/and.poc");
		}

		[Test]
		public void testContains()
		{
			CheckOutput("testing/contains.poc");
		}

		[Test]
		public void testGreater()
		{
			CheckOutput("testing/greater.poc");
		}

		[Test]
		public void testMethod()
		{
			CheckOutput("testing/method.poc");
		}

		[Test]
		public void testNegative()
		{
			CheckOutput("testing/negative.poc");
		}

		[Test]
		public void testNegativeError()
		{
			CheckOutput("testing/negativeError.poc");
		}

		[Test]
		public void testNot()
		{
			CheckOutput("testing/not.poc");
		}

		[Test]
		public void testOr()
		{
			CheckOutput("testing/or.poc");
		}

		[Test]
		public void testPositive()
		{
			CheckOutput("testing/positive.poc");
		}

		[Test]
		public void testPositiveError()
		{
			CheckOutput("testing/positiveError.poc");
		}

	}
}

