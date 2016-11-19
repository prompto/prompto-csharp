using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestPatterns : BaseEParserTest
	{

		[Test]
		public void testIntegerEnumeration()
		{
			compareResourceEME("patterns/integerEnumeration.pec");
		}

		[Test]
		public void testIntegerPattern()
		{
			compareResourceEME("patterns/integerPattern.pec");
		}

		[Test]
		public void testNegativeIntegerRange()
		{
			compareResourceEME("patterns/negativeIntegerRange.pec");
		}

		[Test]
		public void testPositiveIntegerRange()
		{
			compareResourceEME("patterns/positiveIntegerRange.pec");
		}

		[Test]
		public void testTextEnumeration()
		{
			compareResourceEME("patterns/textEnumeration.pec");
		}

		[Test]
		public void testTextPattern()
		{
			compareResourceEME("patterns/textPattern.pec");
		}

	}
}

