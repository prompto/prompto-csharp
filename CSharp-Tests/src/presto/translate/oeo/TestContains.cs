using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestContains : BaseOParserTest
	{

		[Test]
		public void testContainsAllList()
		{
			compareResourceOEO("contains/containsAllList.poc");
		}

		[Test]
		public void testContainsAllSet()
		{
			compareResourceOEO("contains/containsAllSet.poc");
		}

		[Test]
		public void testContainsAllText()
		{
			compareResourceOEO("contains/containsAllText.poc");
		}

		[Test]
		public void testContainsAllTuple()
		{
			compareResourceOEO("contains/containsAllTuple.poc");
		}

		[Test]
		public void testContainsAnyList()
		{
			compareResourceOEO("contains/containsAnyList.poc");
		}

		[Test]
		public void testContainsAnySet()
		{
			compareResourceOEO("contains/containsAnySet.poc");
		}

		[Test]
		public void testContainsAnyText()
		{
			compareResourceOEO("contains/containsAnyText.poc");
		}

		[Test]
		public void testContainsAnyTuple()
		{
			compareResourceOEO("contains/containsAnyTuple.poc");
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

