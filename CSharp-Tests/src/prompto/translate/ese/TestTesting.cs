// generated: 2015-07-05T23:01:01.437
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestTesting : BaseEParserTest
	{

		[Test]
		public void testAnd()
		{
			compareResourceESE("testing/and.pec");
		}

		[Test]
		public void testContains()
		{
			compareResourceESE("testing/contains.pec");
		}

		[Test]
		public void testGreater()
		{
			compareResourceESE("testing/greater.pec");
		}

		[Test]
		public void testMethod()
		{
			compareResourceESE("testing/method.pec");
		}

		[Test]
		public void testNegative()
		{
			compareResourceESE("testing/negative.pec");
		}

		[Test]
		public void testNegativeError()
		{
			compareResourceESE("testing/negativeError.pec");
		}

		[Test]
		public void testNot()
		{
			compareResourceESE("testing/not.pec");
		}

		[Test]
		public void testOr()
		{
			compareResourceESE("testing/or.pec");
		}

		[Test]
		public void testPositive()
		{
			compareResourceESE("testing/positive.pec");
		}

		[Test]
		public void testPositiveError()
		{
			compareResourceESE("testing/positiveError.pec");
		}

	}
}

