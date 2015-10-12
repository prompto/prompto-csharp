using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
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
			CheckOutput("patterns/integerEnumeration.poc");
		}

		[Test]
		public void testIntegerPattern()
		{
			CheckOutput("patterns/integerPattern.poc");
		}

		[Test]
		public void testNegativeIntegerRange()
		{
			CheckOutput("patterns/negativeIntegerRange.poc");
		}

		[Test]
		public void testPositiveIntegerRange()
		{
			CheckOutput("patterns/positiveIntegerRange.poc");
		}

		[Test]
		public void testTextEnumeration()
		{
			CheckOutput("patterns/textEnumeration.poc");
		}

		[Test]
		public void testTextPattern()
		{
			CheckOutput("patterns/textPattern.poc");
		}

	}
}

