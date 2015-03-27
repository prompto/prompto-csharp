using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestTesting : BaseOParserTest
	{

		[Test]
		public void testAnd()
		{
			compareResourceOEO("testing/and.o");
		}

		[Test]
		public void testContains()
		{
			compareResourceOEO("testing/contains.o");
		}

		[Test]
		public void testGreater()
		{
			compareResourceOEO("testing/greater.o");
		}

		[Test]
		public void testMethod()
		{
			compareResourceOEO("testing/method.o");
		}

		[Test]
		public void testNegative()
		{
			compareResourceOEO("testing/negative.o");
		}

		[Test]
		public void testNegativeError()
		{
			compareResourceOEO("testing/negativeError.o");
		}

		[Test]
		public void testNot()
		{
			compareResourceOEO("testing/not.o");
		}

		[Test]
		public void testOr()
		{
			compareResourceOEO("testing/or.o");
		}

		[Test]
		public void testPositive()
		{
			compareResourceOEO("testing/positive.o");
		}

		[Test]
		public void testPositiveError()
		{
			compareResourceOEO("testing/positiveError.o");
		}

	}
}

