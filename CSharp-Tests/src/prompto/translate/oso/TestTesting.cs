using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestTesting : BaseOParserTest
	{

		[Test]
		public void testAnd()
		{
			compareResourceOSO("testing/and.poc");
		}

		[Test]
		public void testContains()
		{
			compareResourceOSO("testing/contains.poc");
		}

		[Test]
		public void testGreater()
		{
			compareResourceOSO("testing/greater.poc");
		}

		[Test]
		public void testMethod()
		{
			compareResourceOSO("testing/method.poc");
		}

		[Test]
		public void testNegative()
		{
			compareResourceOSO("testing/negative.poc");
		}

		[Test]
		public void testNegativeError()
		{
			compareResourceOSO("testing/negativeError.poc");
		}

		[Test]
		public void testNot()
		{
			compareResourceOSO("testing/not.poc");
		}

		[Test]
		public void testOr()
		{
			compareResourceOSO("testing/or.poc");
		}

		[Test]
		public void testPositive()
		{
			compareResourceOSO("testing/positive.poc");
		}

		[Test]
		public void testPositiveError()
		{
			compareResourceOSO("testing/positiveError.poc");
		}

	}
}

