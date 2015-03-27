using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestTesting : BaseOParserTest
	{

		[Test]
		public void testAnd()
		{
			compareResourceOPO("testing/and.o");
		}

		[Test]
		public void testContains()
		{
			compareResourceOPO("testing/contains.o");
		}

		[Test]
		public void testGreater()
		{
			compareResourceOPO("testing/greater.o");
		}

		[Test]
		public void testMethod()
		{
			compareResourceOPO("testing/method.o");
		}

		[Test]
		public void testNegative()
		{
			compareResourceOPO("testing/negative.o");
		}

		[Test]
		public void testNegativeError()
		{
			compareResourceOPO("testing/negativeError.o");
		}

		[Test]
		public void testNot()
		{
			compareResourceOPO("testing/not.o");
		}

		[Test]
		public void testOr()
		{
			compareResourceOPO("testing/or.o");
		}

		[Test]
		public void testPositive()
		{
			compareResourceOPO("testing/positive.o");
		}

		[Test]
		public void testPositiveError()
		{
			compareResourceOPO("testing/positiveError.o");
		}

	}
}

