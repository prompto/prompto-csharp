using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestTesting : BaseEParserTest
	{

		[Test]
		public void testAnd()
		{
			compareResourceEME("testing/and.pec");
		}

		[Test]
		public void testContains()
		{
			compareResourceEME("testing/contains.pec");
		}

		[Test]
		public void testGreater()
		{
			compareResourceEME("testing/greater.pec");
		}

		[Test]
		public void testMethod()
		{
			compareResourceEME("testing/method.pec");
		}

		[Test]
		public void testNegative()
		{
			compareResourceEME("testing/negative.pec");
		}

		[Test]
		public void testNegativeError()
		{
			compareResourceEME("testing/negativeError.pec");
		}

		[Test]
		public void testNot()
		{
			compareResourceEME("testing/not.pec");
		}

		[Test]
		public void testOr()
		{
			compareResourceEME("testing/or.pec");
		}

		[Test]
		public void testPositive()
		{
			compareResourceEME("testing/positive.pec");
		}

		[Test]
		public void testPositiveError()
		{
			compareResourceEME("testing/positiveError.pec");
		}

	}
}

