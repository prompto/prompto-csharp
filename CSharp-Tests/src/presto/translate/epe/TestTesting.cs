using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestTesting : BaseEParserTest
	{

		[Test]
		public void testAnd()
		{
			compareResourceEPE("testing/and.e");
		}

		[Test]
		public void testContains()
		{
			compareResourceEPE("testing/contains.e");
		}

		[Test]
		public void testGreater()
		{
			compareResourceEPE("testing/greater.e");
		}

		[Test]
		public void testMethod()
		{
			compareResourceEPE("testing/method.e");
		}

		[Test]
		public void testNegative()
		{
			compareResourceEPE("testing/negative.e");
		}

		[Test]
		public void testNegativeError()
		{
			compareResourceEPE("testing/negativeError.e");
		}

		[Test]
		public void testNot()
		{
			compareResourceEPE("testing/not.e");
		}

		[Test]
		public void testOr()
		{
			compareResourceEPE("testing/or.e");
		}

		[Test]
		public void testPositive()
		{
			compareResourceEPE("testing/positive.e");
		}

		[Test]
		public void testPositiveError()
		{
			compareResourceEPE("testing/positiveError.e");
		}

	}
}

