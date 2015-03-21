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
			CheckOutput("patterns/integerEnumeration.e");
		}

		[Test]
		public void testIntegerPattern()
		{
			CheckOutput("patterns/integerPattern.e");
		}

		[Test]
		public void testNegativeIntegerRange()
		{
			CheckOutput("patterns/negativeIntegerRange.e");
		}

		[Test]
		public void testPositiveIntegerRange()
		{
			CheckOutput("patterns/positiveIntegerRange.e");
		}

		[Test]
		public void testTextEnumeration()
		{
			CheckOutput("patterns/textEnumeration.e");
		}

		[Test]
		public void testTextPattern()
		{
			CheckOutput("patterns/textPattern.e");
		}

	}
}

