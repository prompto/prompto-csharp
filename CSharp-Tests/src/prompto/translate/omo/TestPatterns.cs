using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestPatterns : BaseOParserTest
	{

		[Test]
		public void testIntegerEnumeration()
		{
			compareResourceOMO("patterns/integerEnumeration.poc");
		}

		[Test]
		public void testIntegerPattern()
		{
			compareResourceOMO("patterns/integerPattern.poc");
		}

		[Test]
		public void testNegativeIntegerRange()
		{
			compareResourceOMO("patterns/negativeIntegerRange.poc");
		}

		[Test]
		public void testPositiveIntegerRange()
		{
			compareResourceOMO("patterns/positiveIntegerRange.poc");
		}

		[Test]
		public void testTextEnumeration()
		{
			compareResourceOMO("patterns/textEnumeration.poc");
		}

		[Test]
		public void testTextPattern()
		{
			compareResourceOMO("patterns/textPattern.poc");
		}

	}
}

