using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestContains : BaseEParserTest
	{

		[Test]
		public void testContainsAllList()
		{
			compareResourceEPE("contains/containsAllList.e");
		}

		[Test]
		public void testContainsAllSet()
		{
			compareResourceEPE("contains/containsAllSet.e");
		}

		[Test]
		public void testContainsAllText()
		{
			compareResourceEPE("contains/containsAllText.e");
		}

		[Test]
		public void testContainsAllTuple()
		{
			compareResourceEPE("contains/containsAllTuple.e");
		}

		[Test]
		public void testContainsAnyList()
		{
			compareResourceEPE("contains/containsAnyList.e");
		}

		[Test]
		public void testContainsAnySet()
		{
			compareResourceEPE("contains/containsAnySet.e");
		}

		[Test]
		public void testContainsAnyText()
		{
			compareResourceEPE("contains/containsAnyText.e");
		}

		[Test]
		public void testContainsAnyTuple()
		{
			compareResourceEPE("contains/containsAnyTuple.e");
		}

		[Test]
		public void testInCharacterRange()
		{
			compareResourceEPE("contains/inCharacterRange.e");
		}

		[Test]
		public void testInDateRange()
		{
			compareResourceEPE("contains/inDateRange.e");
		}

		[Test]
		public void testInDict()
		{
			compareResourceEPE("contains/inDict.e");
		}

		[Test]
		public void testInIntegerRange()
		{
			compareResourceEPE("contains/inIntegerRange.e");
		}

		[Test]
		public void testInList()
		{
			compareResourceEPE("contains/inList.e");
		}

		[Test]
		public void testInSet()
		{
			compareResourceEPE("contains/inSet.e");
		}

		[Test]
		public void testInText()
		{
			compareResourceEPE("contains/inText.e");
		}

		[Test]
		public void testInTimeRange()
		{
			compareResourceEPE("contains/inTimeRange.e");
		}

		[Test]
		public void testInTuple()
		{
			compareResourceEPE("contains/inTuple.e");
		}

		[Test]
		public void testNinCharacterRange()
		{
			compareResourceEPE("contains/ninCharacterRange.e");
		}

		[Test]
		public void testNinDateRange()
		{
			compareResourceEPE("contains/ninDateRange.e");
		}

		[Test]
		public void testNinDict()
		{
			compareResourceEPE("contains/ninDict.e");
		}

		[Test]
		public void testNinIntegerRange()
		{
			compareResourceEPE("contains/ninIntegerRange.e");
		}

		[Test]
		public void testNinList()
		{
			compareResourceEPE("contains/ninList.e");
		}

		[Test]
		public void testNinSet()
		{
			compareResourceEPE("contains/ninSet.e");
		}

		[Test]
		public void testNinText()
		{
			compareResourceEPE("contains/ninText.e");
		}

		[Test]
		public void testNinTimeRange()
		{
			compareResourceEPE("contains/ninTimeRange.e");
		}

	}
}

