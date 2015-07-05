// generated: 2015-07-05T23:01:01.371
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestPatterns : BaseEParserTest
	{

		[Test]
		public void testIntegerEnumeration()
		{
			compareResourceEOE("patterns/integerEnumeration.pec");
		}

		[Test]
		public void testIntegerPattern()
		{
			compareResourceEOE("patterns/integerPattern.pec");
		}

		[Test]
		public void testNegativeIntegerRange()
		{
			compareResourceEOE("patterns/negativeIntegerRange.pec");
		}

		[Test]
		public void testPositiveIntegerRange()
		{
			compareResourceEOE("patterns/positiveIntegerRange.pec");
		}

		[Test]
		public void testTextEnumeration()
		{
			compareResourceEOE("patterns/textEnumeration.pec");
		}

		[Test]
		public void testTextPattern()
		{
			compareResourceEOE("patterns/textPattern.pec");
		}

	}
}

