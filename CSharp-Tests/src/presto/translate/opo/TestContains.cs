using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestContains : BaseOParserTest
	{

		[Test]
		public void testContainsAllList()
		{
			compareResourceOPO("contains/containsAllList.o");
		}

		[Test]
		public void testContainsAllSet()
		{
			compareResourceOPO("contains/containsAllSet.o");
		}

		[Test]
		public void testContainsAllText()
		{
			compareResourceOPO("contains/containsAllText.o");
		}

		[Test]
		public void testContainsAllTuple()
		{
			compareResourceOPO("contains/containsAllTuple.o");
		}

		[Test]
		public void testContainsAnyList()
		{
			compareResourceOPO("contains/containsAnyList.o");
		}

		[Test]
		public void testContainsAnySet()
		{
			compareResourceOPO("contains/containsAnySet.o");
		}

		[Test]
		public void testContainsAnyText()
		{
			compareResourceOPO("contains/containsAnyText.o");
		}

		[Test]
		public void testContainsAnyTuple()
		{
			compareResourceOPO("contains/containsAnyTuple.o");
		}

		[Test]
		public void testInCharacterRange()
		{
			compareResourceOPO("contains/inCharacterRange.o");
		}

		[Test]
		public void testInDateRange()
		{
			compareResourceOPO("contains/inDateRange.o");
		}

		[Test]
		public void testInDict()
		{
			compareResourceOPO("contains/inDict.o");
		}

		[Test]
		public void testInIntegerRange()
		{
			compareResourceOPO("contains/inIntegerRange.o");
		}

		[Test]
		public void testInList()
		{
			compareResourceOPO("contains/inList.o");
		}

		[Test]
		public void testInSet()
		{
			compareResourceOPO("contains/inSet.o");
		}

		[Test]
		public void testInText()
		{
			compareResourceOPO("contains/inText.o");
		}

		[Test]
		public void testInTimeRange()
		{
			compareResourceOPO("contains/inTimeRange.o");
		}

		[Test]
		public void testInTuple()
		{
			compareResourceOPO("contains/inTuple.o");
		}

		[Test]
		public void testNinCharacterRange()
		{
			compareResourceOPO("contains/ninCharacterRange.o");
		}

		[Test]
		public void testNinDateRange()
		{
			compareResourceOPO("contains/ninDateRange.o");
		}

		[Test]
		public void testNinDict()
		{
			compareResourceOPO("contains/ninDict.o");
		}

		[Test]
		public void testNinIntegerRange()
		{
			compareResourceOPO("contains/ninIntegerRange.o");
		}

		[Test]
		public void testNinList()
		{
			compareResourceOPO("contains/ninList.o");
		}

		[Test]
		public void testNinSet()
		{
			compareResourceOPO("contains/ninSet.o");
		}

		[Test]
		public void testNinText()
		{
			compareResourceOPO("contains/ninText.o");
		}

		[Test]
		public void testNinTimeRange()
		{
			compareResourceOPO("contains/ninTimeRange.o");
		}

	}
}

