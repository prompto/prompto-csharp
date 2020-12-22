using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestContains : BaseOParserTest
	{

		[Test]
		public void testHasAllList()
		{
			compareResourceOEO("contains/hasAllList.poc");
		}

		[Test]
		public void testHasAllSet()
		{
			compareResourceOEO("contains/hasAllSet.poc");
		}

		[Test]
		public void testHasAllText()
		{
			compareResourceOEO("contains/hasAllText.poc");
		}

		[Test]
		public void testHasAllTuple()
		{
			compareResourceOEO("contains/hasAllTuple.poc");
		}

		[Test]
		public void testHasAnyList()
		{
			compareResourceOEO("contains/hasAnyList.poc");
		}

		[Test]
		public void testHasAnySet()
		{
			compareResourceOEO("contains/hasAnySet.poc");
		}

		[Test]
		public void testHasAnyText()
		{
			compareResourceOEO("contains/hasAnyText.poc");
		}

		[Test]
		public void testHasAnyTuple()
		{
			compareResourceOEO("contains/hasAnyTuple.poc");
		}

		[Test]
		public void testInCharacterRange()
		{
			compareResourceOEO("contains/inCharacterRange.poc");
		}

		[Test]
		public void testInDateRange()
		{
			compareResourceOEO("contains/inDateRange.poc");
		}

		[Test]
		public void testInDict()
		{
			compareResourceOEO("contains/inDict.poc");
		}

		[Test]
		public void testInIntegerRange()
		{
			compareResourceOEO("contains/inIntegerRange.poc");
		}

		[Test]
		public void testInList()
		{
			compareResourceOEO("contains/inList.poc");
		}

		[Test]
		public void testInSet()
		{
			compareResourceOEO("contains/inSet.poc");
		}

		[Test]
		public void testInText()
		{
			compareResourceOEO("contains/inText.poc");
		}

		[Test]
		public void testInTextEnum()
		{
			compareResourceOEO("contains/inTextEnum.poc");
		}

		[Test]
		public void testInTimeRange()
		{
			compareResourceOEO("contains/inTimeRange.poc");
		}

		[Test]
		public void testInTuple()
		{
			compareResourceOEO("contains/inTuple.poc");
		}

		[Test]
		public void testNinCharacterRange()
		{
			compareResourceOEO("contains/ninCharacterRange.poc");
		}

		[Test]
		public void testNinDateRange()
		{
			compareResourceOEO("contains/ninDateRange.poc");
		}

		[Test]
		public void testNinDict()
		{
			compareResourceOEO("contains/ninDict.poc");
		}

		[Test]
		public void testNinIntegerRange()
		{
			compareResourceOEO("contains/ninIntegerRange.poc");
		}

		[Test]
		public void testNinList()
		{
			compareResourceOEO("contains/ninList.poc");
		}

		[Test]
		public void testNinSet()
		{
			compareResourceOEO("contains/ninSet.poc");
		}

		[Test]
		public void testNinText()
		{
			compareResourceOEO("contains/ninText.poc");
		}

		[Test]
		public void testNinTimeRange()
		{
			compareResourceOEO("contains/ninTimeRange.poc");
		}

	}
}

