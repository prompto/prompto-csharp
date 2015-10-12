using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestTesting : BaseOParserTest
	{

		[Test]
		public void testAnd()
		{
			compareResourceOEO("testing/and.poc");
		}

		[Test]
		public void testContains()
		{
			compareResourceOEO("testing/contains.poc");
		}

		[Test]
		public void testGreater()
		{
			compareResourceOEO("testing/greater.poc");
		}

		[Test]
		public void testMethod()
		{
			compareResourceOEO("testing/method.poc");
		}

		[Test]
		public void testNegative()
		{
			compareResourceOEO("testing/negative.poc");
		}

		[Test]
		public void testNegativeError()
		{
			compareResourceOEO("testing/negativeError.poc");
		}

		[Test]
		public void testNot()
		{
			compareResourceOEO("testing/not.poc");
		}

		[Test]
		public void testOr()
		{
			compareResourceOEO("testing/or.poc");
		}

		[Test]
		public void testPositive()
		{
			compareResourceOEO("testing/positive.poc");
		}

		[Test]
		public void testPositiveError()
		{
			compareResourceOEO("testing/positiveError.poc");
		}

	}
}

