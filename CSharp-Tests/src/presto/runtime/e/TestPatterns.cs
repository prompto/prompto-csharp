using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestPatterns : BaseEParserTest
	{

		[SetUp]
		public void before()
		{
			Out.init();
		}

		[TearDown]
		public void after()
		{
			Out.restore();
		}

		[Test]
		public void testIntegerEnumeration()
		{
			CheckOutput("patterns/integerEnumeration.pec");
		}

		[Test]
		public void testIntegerPattern()
		{
			CheckOutput("patterns/integerPattern.pec");
		}

		[Test]
		public void testNegativeIntegerRange()
		{
			CheckOutput("patterns/negativeIntegerRange.pec");
		}

		[Test]
		public void testPositiveIntegerRange()
		{
			CheckOutput("patterns/positiveIntegerRange.pec");
		}

		[Test]
		public void testTextEnumeration()
		{
			CheckOutput("patterns/textEnumeration.pec");
		}

		[Test]
		public void testTextPattern()
		{
			CheckOutput("patterns/textPattern.pec");
		}

	}
}

