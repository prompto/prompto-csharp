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
			compareResourceEOE("testing/and.e");
		}

		[Test]
		public void testContains()
		{
			compareResourceEOE("testing/contains.e");
		}

		[Test]
		public void testGreater()
		{
			compareResourceEOE("testing/greater.e");
		}

		[Test]
		public void testMethod()
		{
			compareResourceEOE("testing/method.e");
		}

		[Test]
		public void testNegative()
		{
			compareResourceEOE("testing/negative.e");
		}

		[Test]
		public void testNegativeError()
		{
			compareResourceEOE("testing/negativeError.e");
		}

		[Test]
		public void testNot()
		{
			compareResourceEOE("testing/not.e");
		}

		[Test]
		public void testOr()
		{
			compareResourceEOE("testing/or.e");
		}

		[Test]
		public void testPositive()
		{
			compareResourceEOE("testing/positive.e");
		}

		[Test]
		public void testPositiveError()
		{
			compareResourceEOE("testing/positiveError.e");
		}

	}
}

