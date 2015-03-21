using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestContains : BaseEParserTest
	{

		[Test]
		public void testContainsAllList()
		{
			compareResourceEOE("contains/containsAllList.e");
		}

		[Test]
		public void testContainsAllSet()
		{
			compareResourceEOE("contains/containsAllSet.e");
		}

		[Test]
		public void testContainsAllText()
		{
			compareResourceEOE("contains/containsAllText.e");
		}

		[Test]
		public void testContainsAllTuple()
		{
			compareResourceEOE("contains/containsAllTuple.e");
		}

		[Test]
		public void testContainsAnyList()
		{
			compareResourceEOE("contains/containsAnyList.e");
		}

		[Test]
		public void testContainsAnySet()
		{
			compareResourceEOE("contains/containsAnySet.e");
		}

		[Test]
		public void testContainsAnyText()
		{
			compareResourceEOE("contains/containsAnyText.e");
		}

		[Test]
		public void testContainsAnyTuple()
		{
			compareResourceEOE("contains/containsAnyTuple.e");
		}

		[Test]
		public void testInCharacterRange()
		{
			compareResourceEOE("contains/inCharacterRange.e");
		}

		[Test]
		public void testInDateRange()
		{
			compareResourceEOE("contains/inDateRange.e");
		}

		[Test]
		public void testInDict()
		{
			compareResourceEOE("contains/inDict.e");
		}

		[Test]
		public void testInIntegerRange()
		{
			compareResourceEOE("contains/inIntegerRange.e");
		}

		[Test]
		public void testInList()
		{
			compareResourceEOE("contains/inList.e");
		}

		[Test]
		public void testInSet()
		{
			compareResourceEOE("contains/inSet.e");
		}

		[Test]
		public void testInText()
		{
			compareResourceEOE("contains/inText.e");
		}

		[Test]
		public void testInTimeRange()
		{
			compareResourceEOE("contains/inTimeRange.e");
		}

		[Test]
		public void testInTuple()
		{
			compareResourceEOE("contains/inTuple.e");
		}

		[Test]
		public void testNinCharacterRange()
		{
			compareResourceEOE("contains/ninCharacterRange.e");
		}

		[Test]
		public void testNinDateRange()
		{
			compareResourceEOE("contains/ninDateRange.e");
		}

		[Test]
		public void testNinDict()
		{
			compareResourceEOE("contains/ninDict.e");
		}

		[Test]
		public void testNinIntegerRange()
		{
			compareResourceEOE("contains/ninIntegerRange.e");
		}

		[Test]
		public void testNinList()
		{
			compareResourceEOE("contains/ninList.e");
		}

		[Test]
		public void testNinSet()
		{
			compareResourceEOE("contains/ninSet.e");
		}

		[Test]
		public void testNinText()
		{
			compareResourceEOE("contains/ninText.e");
		}

		[Test]
		public void testNinTimeRange()
		{
			compareResourceEOE("contains/ninTimeRange.e");
		}

	}
}

