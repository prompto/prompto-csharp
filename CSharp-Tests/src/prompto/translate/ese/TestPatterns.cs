// generated: 2015-07-05T23:01:01.373
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
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

