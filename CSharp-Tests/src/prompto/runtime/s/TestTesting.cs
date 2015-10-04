// generated: 2015-10-05T01:03:20.263
using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.s
{

	[TestFixture]
	public class TestTesting : BaseSParserTest
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
			CheckOutput("testing/and.psc");
		}

		[Test]
		public void testContains()
		{
			CheckOutput("testing/contains.psc");
		}

		[Test]
		public void testGreater()
		{
			CheckOutput("testing/greater.psc");
		}

		[Test]
		public void testMethod()
		{
			CheckOutput("testing/method.psc");
		}

		[Test]
		public void testNegative()
		{
			CheckOutput("testing/negative.psc");
		}

		[Test]
		public void testNegativeError()
		{
			CheckOutput("testing/negativeError.psc");
		}

		[Test]
		public void testNot()
		{
			CheckOutput("testing/not.psc");
		}

		[Test]
		public void testOr()
		{
			CheckOutput("testing/or.psc");
		}

		[Test]
		public void testPositive()
		{
			CheckOutput("testing/positive.psc");
		}

		[Test]
		public void testPositiveError()
		{
			CheckOutput("testing/positiveError.psc");
		}

	}
}

