using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestPatterns : BaseEParserTest
	{

		[Test]
		public void testIntegerEnumeration()
		{
			compareResourceEPE("patterns/integerEnumeration.e");
		}

		[Test]
		public void testIntegerPattern()
		{
			compareResourceEPE("patterns/integerPattern.e");
		}

		[Test]
		public void testNegativeIntegerRange()
		{
			compareResourceEPE("patterns/negativeIntegerRange.e");
		}

		[Test]
		public void testPositiveIntegerRange()
		{
			compareResourceEPE("patterns/positiveIntegerRange.e");
		}

		[Test]
		public void testTextEnumeration()
		{
			compareResourceEPE("patterns/textEnumeration.e");
		}

		[Test]
		public void testTextPattern()
		{
			compareResourceEPE("patterns/textPattern.e");
		}

	}
}

