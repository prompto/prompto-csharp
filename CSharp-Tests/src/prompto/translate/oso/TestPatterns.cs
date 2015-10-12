using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestPatterns : BaseOParserTest
	{

		[Test]
		public void testIntegerEnumeration()
		{
			compareResourceOSO("patterns/integerEnumeration.poc");
		}

		[Test]
		public void testIntegerPattern()
		{
			compareResourceOSO("patterns/integerPattern.poc");
		}

		[Test]
		public void testNegativeIntegerRange()
		{
			compareResourceOSO("patterns/negativeIntegerRange.poc");
		}

		[Test]
		public void testPositiveIntegerRange()
		{
			compareResourceOSO("patterns/positiveIntegerRange.poc");
		}

		[Test]
		public void testTextEnumeration()
		{
			compareResourceOSO("patterns/textEnumeration.poc");
		}

		[Test]
		public void testTextPattern()
		{
			compareResourceOSO("patterns/textPattern.poc");
		}

	}
}

