using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
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
			CheckOutput("testing/and.pec");
		}

		[Test]
		public void testContains()
		{
			CheckOutput("testing/contains.pec");
		}

		[Test]
		public void testGreater()
		{
			CheckOutput("testing/greater.pec");
		}

		[Test]
		public void testMethod()
		{
			CheckOutput("testing/method.pec");
		}

		[Test]
		public void testNegative()
		{
			CheckOutput("testing/negative.pec");
		}

		[Test]
		public void testNegativeError()
		{
			CheckOutput("testing/negativeError.pec");
		}

		[Test]
		public void testNot()
		{
			CheckOutput("testing/not.pec");
		}

		[Test]
		public void testOr()
		{
			CheckOutput("testing/or.pec");
		}

		[Test]
		public void testPositive()
		{
			CheckOutput("testing/positive.pec");
		}

		[Test]
		public void testPositiveError()
		{
			CheckOutput("testing/positiveError.pec");
		}

	}
}

