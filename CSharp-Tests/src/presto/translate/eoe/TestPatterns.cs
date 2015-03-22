using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestPatterns : BaseEParserTest
	{

		[Test]
		public void testIntegerEnumeration()
		{
			compareResourceEOE("patterns/integerEnumeration.e");
		}

		[Test]
		public void testIntegerPattern()
		{
			compareResourceEOE("patterns/integerPattern.e");
		}

		[Test]
		public void testNegativeIntegerRange()
		{
			compareResourceEOE("patterns/negativeIntegerRange.e");
		}

		[Test]
		public void testPositiveIntegerRange()
		{
			compareResourceEOE("patterns/positiveIntegerRange.e");
		}

		[Test]
		public void testTextEnumeration()
		{
			compareResourceEOE("patterns/textEnumeration.e");
		}

		[Test]
		public void testTextPattern()
		{
			compareResourceEOE("patterns/textPattern.e");
		}

	}
}

