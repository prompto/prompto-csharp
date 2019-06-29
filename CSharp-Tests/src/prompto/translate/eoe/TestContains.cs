using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestContains : BaseEParserTest
	{

		[Test]
		public void testContainsAllDict()
		{
			compareResourceEOE("contains/containsAllDict.pec");
		}

		[Test]
		public void testContainsAllList()
		{
			compareResourceEOE("contains/containsAllList.pec");
		}

		[Test]
		public void testContainsAllRange()
		{
			compareResourceEOE("contains/containsAllRange.pec");
		}

		[Test]
		public void testContainsAllSet()
		{
			compareResourceEOE("contains/containsAllSet.pec");
		}

		[Test]
		public void testContainsAllText()
		{
			compareResourceEOE("contains/containsAllText.pec");
		}

		[Test]
		public void testContainsAllTuple()
		{
			compareResourceEOE("contains/containsAllTuple.pec");
		}

		[Test]
		public void testContainsAnyDict()
		{
			compareResourceEOE("contains/containsAnyDict.pec");
		}

		[Test]
		public void testContainsAnyList()
		{
			compareResourceEOE("contains/containsAnyList.pec");
		}

		[Test]
		public void testContainsAnyRange()
		{
			compareResourceEOE("contains/containsAnyRange.pec");
		}

		[Test]
		public void testContainsAnySet()
		{
			compareResourceEOE("contains/containsAnySet.pec");
		}

		[Test]
		public void testContainsAnyText()
		{
			compareResourceEOE("contains/containsAnyText.pec");
		}

		[Test]
		public void testContainsAnyTuple()
		{
			compareResourceEOE("contains/containsAnyTuple.pec");
		}

		[Test]
		public void testInCharacterRange()
		{
			compareResourceEOE("contains/inCharacterRange.pec");
		}

		[Test]
		public void testInDateRange()
		{
			compareResourceEOE("contains/inDateRange.pec");
		}

		[Test]
		public void testInDict()
		{
			compareResourceEOE("contains/inDict.pec");
		}

		[Test]
		public void testInIntegerRange()
		{
			compareResourceEOE("contains/inIntegerRange.pec");
		}

		[Test]
		public void testInList()
		{
			compareResourceEOE("contains/inList.pec");
		}

		[Test]
		public void testInSet()
		{
			compareResourceEOE("contains/inSet.pec");
		}

		[Test]
		public void testInText()
		{
			compareResourceEOE("contains/inText.pec");
		}

		[Test]
		public void testInTextEnum()
		{
			compareResourceEOE("contains/inTextEnum.pec");
		}

		[Test]
		public void testInTimeRange()
		{
			compareResourceEOE("contains/inTimeRange.pec");
		}

		[Test]
		public void testInTuple()
		{
			compareResourceEOE("contains/inTuple.pec");
		}

		[Test]
		public void testNinCharacterRange()
		{
			compareResourceEOE("contains/ninCharacterRange.pec");
		}

		[Test]
		public void testNinDateRange()
		{
			compareResourceEOE("contains/ninDateRange.pec");
		}

		[Test]
		public void testNinDict()
		{
			compareResourceEOE("contains/ninDict.pec");
		}

		[Test]
		public void testNinIntegerRange()
		{
			compareResourceEOE("contains/ninIntegerRange.pec");
		}

		[Test]
		public void testNinList()
		{
			compareResourceEOE("contains/ninList.pec");
		}

		[Test]
		public void testNinSet()
		{
			compareResourceEOE("contains/ninSet.pec");
		}

		[Test]
		public void testNinText()
		{
			compareResourceEOE("contains/ninText.pec");
		}

		[Test]
		public void testNinTimeRange()
		{
			compareResourceEOE("contains/ninTimeRange.pec");
		}

	}
}

