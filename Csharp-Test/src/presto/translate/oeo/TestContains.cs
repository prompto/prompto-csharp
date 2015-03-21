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
			compareResourceOEO("contains/containsAllList.o");
		}

		[Test]
		public void testContainsAllSet()
		{
			compareResourceOEO("contains/containsAllSet.o");
		}

		[Test]
		public void testContainsAllText()
		{
			compareResourceOEO("contains/containsAllText.o");
		}

		[Test]
		public void testContainsAllTuple()
		{
			compareResourceOEO("contains/containsAllTuple.o");
		}

		[Test]
		public void testContainsAnyList()
		{
			compareResourceOEO("contains/containsAnyList.o");
		}

		[Test]
		public void testContainsAnySet()
		{
			compareResourceOEO("contains/containsAnySet.o");
		}

		[Test]
		public void testContainsAnyText()
		{
			compareResourceOEO("contains/containsAnyText.o");
		}

		[Test]
		public void testContainsAnyTuple()
		{
			compareResourceOEO("contains/containsAnyTuple.o");
		}

		[Test]
		public void testInCharacterRange()
		{
			compareResourceOEO("contains/inCharacterRange.o");
		}

		[Test]
		public void testInDateRange()
		{
			compareResourceOEO("contains/inDateRange.o");
		}

		[Test]
		public void testInDict()
		{
			compareResourceOEO("contains/inDict.o");
		}

		[Test]
		public void testInIntegerRange()
		{
			compareResourceOEO("contains/inIntegerRange.o");
		}

		[Test]
		public void testInList()
		{
			compareResourceOEO("contains/inList.o");
		}

		[Test]
		public void testInSet()
		{
			compareResourceOEO("contains/inSet.o");
		}

		[Test]
		public void testInText()
		{
			compareResourceOEO("contains/inText.o");
		}

		[Test]
		public void testInTimeRange()
		{
			compareResourceOEO("contains/inTimeRange.o");
		}

		[Test]
		public void testInTuple()
		{
			compareResourceOEO("contains/inTuple.o");
		}

		[Test]
		public void testNinCharacterRange()
		{
			compareResourceOEO("contains/ninCharacterRange.o");
		}

		[Test]
		public void testNinDateRange()
		{
			compareResourceOEO("contains/ninDateRange.o");
		}

		[Test]
		public void testNinDict()
		{
			compareResourceOEO("contains/ninDict.o");
		}

		[Test]
		public void testNinIntegerRange()
		{
			compareResourceOEO("contains/ninIntegerRange.o");
		}

		[Test]
		public void testNinList()
		{
			compareResourceOEO("contains/ninList.o");
		}

		[Test]
		public void testNinSet()
		{
			compareResourceOEO("contains/ninSet.o");
		}

		[Test]
		public void testNinText()
		{
			compareResourceOEO("contains/ninText.o");
		}

		[Test]
		public void testNinTimeRange()
		{
			compareResourceOEO("contains/ninTimeRange.o");
		}

	}
}

