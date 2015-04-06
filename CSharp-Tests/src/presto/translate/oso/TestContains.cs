using NUnit.Framework;
using presto.parser;

namespace presto.translate.oso
{

	[TestFixture]
	public class TestContains : BaseOParserTest
	{

		[Test]
		public void testContainsAllList()
		{
			compareResourceOSO("contains/containsAllList.poc");
		}

		[Test]
		public void testContainsAllSet()
		{
			compareResourceOSO("contains/containsAllSet.poc");
		}

		[Test]
		public void testContainsAllText()
		{
			compareResourceOSO("contains/containsAllText.poc");
		}

		[Test]
		public void testContainsAllTuple()
		{
			compareResourceOSO("contains/containsAllTuple.poc");
		}

		[Test]
		public void testContainsAnyList()
		{
			compareResourceOSO("contains/containsAnyList.poc");
		}

		[Test]
		public void testContainsAnySet()
		{
			compareResourceOSO("contains/containsAnySet.poc");
		}

		[Test]
		public void testContainsAnyText()
		{
			compareResourceOSO("contains/containsAnyText.poc");
		}

		[Test]
		public void testContainsAnyTuple()
		{
			compareResourceOSO("contains/containsAnyTuple.poc");
		}

		[Test]
		public void testInCharacterRange()
		{
			compareResourceOSO("contains/inCharacterRange.poc");
		}

		[Test]
		public void testInDateRange()
		{
			compareResourceOSO("contains/inDateRange.poc");
		}

		[Test]
		public void testInDict()
		{
			compareResourceOSO("contains/inDict.poc");
		}

		[Test]
		public void testInIntegerRange()
		{
			compareResourceOSO("contains/inIntegerRange.poc");
		}

		[Test]
		public void testInList()
		{
			compareResourceOSO("contains/inList.poc");
		}

		[Test]
		public void testInSet()
		{
			compareResourceOSO("contains/inSet.poc");
		}

		[Test]
		public void testInText()
		{
			compareResourceOSO("contains/inText.poc");
		}

		[Test]
		public void testInTimeRange()
		{
			compareResourceOSO("contains/inTimeRange.poc");
		}

		[Test]
		public void testInTuple()
		{
			compareResourceOSO("contains/inTuple.poc");
		}

		[Test]
		public void testNinCharacterRange()
		{
			compareResourceOSO("contains/ninCharacterRange.poc");
		}

		[Test]
		public void testNinDateRange()
		{
			compareResourceOSO("contains/ninDateRange.poc");
		}

		[Test]
		public void testNinDict()
		{
			compareResourceOSO("contains/ninDict.poc");
		}

		[Test]
		public void testNinIntegerRange()
		{
			compareResourceOSO("contains/ninIntegerRange.poc");
		}

		[Test]
		public void testNinList()
		{
			compareResourceOSO("contains/ninList.poc");
		}

		[Test]
		public void testNinSet()
		{
			compareResourceOSO("contains/ninSet.poc");
		}

		[Test]
		public void testNinText()
		{
			compareResourceOSO("contains/ninText.poc");
		}

		[Test]
		public void testNinTimeRange()
		{
			compareResourceOSO("contains/ninTimeRange.poc");
		}

	}
}

