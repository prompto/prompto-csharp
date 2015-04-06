using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
{

	[TestFixture]
	public class TestPatterns : BaseEParserTest
	{

		[Test]
		public void testIntegerEnumeration()
		{
			compareResourceESE("patterns/integerEnumeration.pec");
		}

		[Test]
		public void testIntegerPattern()
		{
			compareResourceESE("patterns/integerPattern.pec");
		}

		[Test]
		public void testNegativeIntegerRange()
		{
			compareResourceESE("patterns/negativeIntegerRange.pec");
		}

		[Test]
		public void testPositiveIntegerRange()
		{
			compareResourceESE("patterns/positiveIntegerRange.pec");
		}

		[Test]
		public void testTextEnumeration()
		{
			compareResourceESE("patterns/textEnumeration.pec");
		}

		[Test]
		public void testTextPattern()
		{
			compareResourceESE("patterns/textPattern.pec");
		}

	}
}

