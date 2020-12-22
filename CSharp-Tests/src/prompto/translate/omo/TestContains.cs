using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestContains : BaseOParserTest
	{

		[Test]
		public void testHasAllList()
		{
			compareResourceOMO("contains/hasAllList.poc");
		}

		[Test]
		public void testHasAllSet()
		{
			compareResourceOMO("contains/hasAllSet.poc");
		}

		[Test]
		public void testHasAllText()
		{
			compareResourceOMO("contains/hasAllText.poc");
		}

		[Test]
		public void testHasAllTuple()
		{
			compareResourceOMO("contains/hasAllTuple.poc");
		}

		[Test]
		public void testHasAnyList()
		{
			compareResourceOMO("contains/hasAnyList.poc");
		}

		[Test]
		public void testHasAnySet()
		{
			compareResourceOMO("contains/hasAnySet.poc");
		}

		[Test]
		public void testHasAnyText()
		{
			compareResourceOMO("contains/hasAnyText.poc");
		}

		[Test]
		public void testHasAnyTuple()
		{
			compareResourceOMO("contains/hasAnyTuple.poc");
		}

		[Test]
		public void testInCharacterRange()
		{
			compareResourceOMO("contains/inCharacterRange.poc");
		}

		[Test]
		public void testInDateRange()
		{
			compareResourceOMO("contains/inDateRange.poc");
		}

		[Test]
		public void testInDict()
		{
			compareResourceOMO("contains/inDict.poc");
		}

		[Test]
		public void testInIntegerRange()
		{
			compareResourceOMO("contains/inIntegerRange.poc");
		}

		[Test]
		public void testInList()
		{
			compareResourceOMO("contains/inList.poc");
		}

		[Test]
		public void testInSet()
		{
			compareResourceOMO("contains/inSet.poc");
		}

		[Test]
		public void testInText()
		{
			compareResourceOMO("contains/inText.poc");
		}

		[Test]
		public void testInTextEnum()
		{
			compareResourceOMO("contains/inTextEnum.poc");
		}

		[Test]
		public void testInTimeRange()
		{
			compareResourceOMO("contains/inTimeRange.poc");
		}

		[Test]
		public void testInTuple()
		{
			compareResourceOMO("contains/inTuple.poc");
		}

		[Test]
		public void testNinCharacterRange()
		{
			compareResourceOMO("contains/ninCharacterRange.poc");
		}

		[Test]
		public void testNinDateRange()
		{
			compareResourceOMO("contains/ninDateRange.poc");
		}

		[Test]
		public void testNinDict()
		{
			compareResourceOMO("contains/ninDict.poc");
		}

		[Test]
		public void testNinIntegerRange()
		{
			compareResourceOMO("contains/ninIntegerRange.poc");
		}

		[Test]
		public void testNinList()
		{
			compareResourceOMO("contains/ninList.poc");
		}

		[Test]
		public void testNinSet()
		{
			compareResourceOMO("contains/ninSet.poc");
		}

		[Test]
		public void testNinText()
		{
			compareResourceOMO("contains/ninText.poc");
		}

		[Test]
		public void testNinTimeRange()
		{
			compareResourceOMO("contains/ninTimeRange.poc");
		}

	}
}

