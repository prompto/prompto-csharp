using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestPatterns : BaseOParserTest
	{

		[Test]
		public void testIntegerEnumeration()
		{
			compareResourceOEO("patterns/integerEnumeration.o");
		}

		[Test]
		public void testIntegerPattern()
		{
			compareResourceOEO("patterns/integerPattern.o");
		}

		[Test]
		public void testNegativeIntegerRange()
		{
			compareResourceOEO("patterns/negativeIntegerRange.o");
		}

		[Test]
		public void testPositiveIntegerRange()
		{
			compareResourceOEO("patterns/positiveIntegerRange.o");
		}

		[Test]
		public void testTextEnumeration()
		{
			compareResourceOEO("patterns/textEnumeration.o");
		}

		[Test]
		public void testTextPattern()
		{
			compareResourceOEO("patterns/textPattern.o");
		}

	}
}

