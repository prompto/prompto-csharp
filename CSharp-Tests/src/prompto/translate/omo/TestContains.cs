using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestContains : BaseOParserTest
	{

		[Test]
		public void testContainsAllList()
		{
			compareResourceOMO("contains/containsAllList.poc");
		}

		[Test]
		public void testContainsAllSet()
		{
			compareResourceOMO("contains/containsAllSet.poc");
		}

		[Test]
		public void testContainsAllText()
		{
			compareResourceOMO("contains/containsAllText.poc");
		}

		[Test]
		public void testContainsAllTuple()
		{
			compareResourceOMO("contains/containsAllTuple.poc");
		}

		[Test]
		public void testContainsAnyList()
		{
			compareResourceOMO("contains/containsAnyList.poc");
		}

		[Test]
		public void testContainsAnySet()
		{
			compareResourceOMO("contains/containsAnySet.poc");
		}

		[Test]
		public void testContainsAnyText()
		{
			compareResourceOMO("contains/containsAnyText.poc");
		}

		[Test]
		public void testContainsAnyTuple()
		{
			compareResourceOMO("contains/containsAnyTuple.poc");
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

