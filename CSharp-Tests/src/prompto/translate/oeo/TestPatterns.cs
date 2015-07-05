// generated: 2015-07-05T23:01:01.375
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestPatterns : BaseOParserTest
	{

		[Test]
		public void testIntegerEnumeration()
		{
			compareResourceOEO("patterns/integerEnumeration.poc");
		}

		[Test]
		public void testIntegerPattern()
		{
			compareResourceOEO("patterns/integerPattern.poc");
		}

		[Test]
		public void testNegativeIntegerRange()
		{
			compareResourceOEO("patterns/negativeIntegerRange.poc");
		}

		[Test]
		public void testPositiveIntegerRange()
		{
			compareResourceOEO("patterns/positiveIntegerRange.poc");
		}

		[Test]
		public void testTextEnumeration()
		{
			compareResourceOEO("patterns/textEnumeration.poc");
		}

		[Test]
		public void testTextPattern()
		{
			compareResourceOEO("patterns/textPattern.poc");
		}

	}
}

