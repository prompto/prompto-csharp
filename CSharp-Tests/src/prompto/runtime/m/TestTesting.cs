using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.m
{

	[TestFixture]
	public class TestTesting : BaseMParserTest
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
			CheckOutput("testing/and.pmc");
		}

		[Test]
		public void testContains()
		{
			CheckOutput("testing/contains.pmc");
		}

		[Test]
		public void testGreater()
		{
			CheckOutput("testing/greater.pmc");
		}

		[Test]
		public void testMethod()
		{
			CheckOutput("testing/method.pmc");
		}

		[Test]
		public void testNegative()
		{
			CheckOutput("testing/negative.pmc");
		}

		[Test]
		public void testNegativeError()
		{
			CheckOutput("testing/negativeError.pmc");
		}

		[Test]
		public void testNot()
		{
			CheckOutput("testing/not.pmc");
		}

		[Test]
		public void testOr()
		{
			CheckOutput("testing/or.pmc");
		}

		[Test]
		public void testPositive()
		{
			CheckOutput("testing/positive.pmc");
		}

		[Test]
		public void testPositiveError()
		{
			CheckOutput("testing/positiveError.pmc");
		}

	}
}

