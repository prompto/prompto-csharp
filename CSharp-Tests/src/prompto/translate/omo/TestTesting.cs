using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestTesting : BaseOParserTest
	{

		[Test]
		public void testAnd()
		{
			compareResourceOMO("testing/and.poc");
		}

		[Test]
		public void testContains()
		{
			compareResourceOMO("testing/contains.poc");
		}

		[Test]
		public void testGreater()
		{
			compareResourceOMO("testing/greater.poc");
		}

		[Test]
		public void testMethod()
		{
			compareResourceOMO("testing/method.poc");
		}

		[Test]
		public void testNegative()
		{
			compareResourceOMO("testing/negative.poc");
		}

		[Test]
		public void testNegativeError()
		{
			compareResourceOMO("testing/negativeError.poc");
		}

		[Test]
		public void testNot()
		{
			compareResourceOMO("testing/not.poc");
		}

		[Test]
		public void testOr()
		{
			compareResourceOMO("testing/or.poc");
		}

		[Test]
		public void testPositive()
		{
			compareResourceOMO("testing/positive.poc");
		}

		[Test]
		public void testPositiveError()
		{
			compareResourceOMO("testing/positiveError.poc");
		}

	}
}

