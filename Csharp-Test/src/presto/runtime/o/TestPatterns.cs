using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestPatterns : BaseOParserTest
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
			CheckOutput("patterns/integerEnumeration.o");
		}

		[Test]
		public void testIntegerPattern()
		{
			CheckOutput("patterns/integerPattern.o");
		}

		[Test]
		public void testNegativeIntegerRange()
		{
			CheckOutput("patterns/negativeIntegerRange.o");
		}

		[Test]
		public void testPositiveIntegerRange()
		{
			CheckOutput("patterns/positiveIntegerRange.o");
		}

		[Test]
		public void testTextEnumeration()
		{
			CheckOutput("patterns/textEnumeration.o");
		}

		[Test]
		public void testTextPattern()
		{
			CheckOutput("patterns/textPattern.o");
		}

	}
}

