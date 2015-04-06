using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestTesting : BaseEParserTest
	{

		[Test]
		public void testAnd()
		{
			compareResourceEOE("testing/and.pec");
		}

		[Test]
		public void testContains()
		{
			compareResourceEOE("testing/contains.pec");
		}

		[Test]
		public void testGreater()
		{
			compareResourceEOE("testing/greater.pec");
		}

		[Test]
		public void testMethod()
		{
			compareResourceEOE("testing/method.pec");
		}

		[Test]
		public void testNegative()
		{
			compareResourceEOE("testing/negative.pec");
		}

		[Test]
		public void testNegativeError()
		{
			compareResourceEOE("testing/negativeError.pec");
		}

		[Test]
		public void testNot()
		{
			compareResourceEOE("testing/not.pec");
		}

		[Test]
		public void testOr()
		{
			compareResourceEOE("testing/or.pec");
		}

		[Test]
		public void testPositive()
		{
			compareResourceEOE("testing/positive.pec");
		}

		[Test]
		public void testPositiveError()
		{
			compareResourceEOE("testing/positiveError.pec");
		}

	}
}

