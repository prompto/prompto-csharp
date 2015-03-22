using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestPatterns : BaseOParserTest
	{

		[Test]
		public void testIntegerEnumeration()
		{
			compareResourceOPO("patterns/integerEnumeration.o");
		}

		[Test]
		public void testIntegerPattern()
		{
			compareResourceOPO("patterns/integerPattern.o");
		}

		[Test]
		public void testNegativeIntegerRange()
		{
			compareResourceOPO("patterns/negativeIntegerRange.o");
		}

		[Test]
		public void testPositiveIntegerRange()
		{
			compareResourceOPO("patterns/positiveIntegerRange.o");
		}

		[Test]
		public void testTextEnumeration()
		{
			compareResourceOPO("patterns/textEnumeration.o");
		}

		[Test]
		public void testTextPattern()
		{
			compareResourceOPO("patterns/textPattern.o");
		}

	}
}

