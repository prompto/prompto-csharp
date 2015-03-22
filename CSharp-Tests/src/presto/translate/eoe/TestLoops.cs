using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestLoops : BaseEParserTest
	{

		[Test]
		public void testDoWhile()
		{
			compareResourceEOE("loops/doWhile.e");
		}

		[Test]
		public void testForEachCharacterRange()
		{
			compareResourceEOE("loops/forEachCharacterRange.e");
		}

		[Test]
		public void testForEachCharacterRangeWithIndex()
		{
			compareResourceEOE("loops/forEachCharacterRangeWithIndex.e");
		}

		[Test]
		public void testForEachDateRange()
		{
			compareResourceEOE("loops/forEachDateRange.e");
		}

		[Test]
		public void testForEachDateRangeWithIndex()
		{
			compareResourceEOE("loops/forEachDateRangeWithIndex.e");
		}

		[Test]
		public void testForEachDictionaryItem()
		{
			compareResourceEOE("loops/forEachDictionaryItem.e");
		}

		[Test]
		public void testForEachDictionaryItemWithIndex()
		{
			compareResourceEOE("loops/forEachDictionaryItemWithIndex.e");
		}

		[Test]
		public void testForEachDictionaryKey()
		{
			compareResourceEOE("loops/forEachDictionaryKey.e");
		}

		[Test]
		public void testForEachDictionaryKeyWithIndex()
		{
			compareResourceEOE("loops/forEachDictionaryKeyWithIndex.e");
		}

		[Test]
		public void testForEachDictionaryValue()
		{
			compareResourceEOE("loops/forEachDictionaryValue.e");
		}

		[Test]
		public void testForEachDictionaryValueWithIndex()
		{
			compareResourceEOE("loops/forEachDictionaryValueWithIndex.e");
		}

		[Test]
		public void testForEachInstanceList()
		{
			compareResourceEOE("loops/forEachInstanceList.e");
		}

		[Test]
		public void testForEachInstanceListWithIndex()
		{
			compareResourceEOE("loops/forEachInstanceListWithIndex.e");
		}

		[Test]
		public void testForEachInstanceSet()
		{
			compareResourceEOE("loops/forEachInstanceSet.e");
		}

		[Test]
		public void testForEachInstanceSetWithIndex()
		{
			compareResourceEOE("loops/forEachInstanceSetWithIndex.e");
		}

		[Test]
		public void testForEachIntegerList()
		{
			compareResourceEOE("loops/forEachIntegerList.e");
		}

		[Test]
		public void testForEachIntegerListWithIndex()
		{
			compareResourceEOE("loops/forEachIntegerListWithIndex.e");
		}

		[Test]
		public void testForEachIntegerRange()
		{
			compareResourceEOE("loops/forEachIntegerRange.e");
		}

		[Test]
		public void testForEachIntegerRangeWithIndex()
		{
			compareResourceEOE("loops/forEachIntegerRangeWithIndex.e");
		}

		[Test]
		public void testForEachIntegerSet()
		{
			compareResourceEOE("loops/forEachIntegerSet.e");
		}

		[Test]
		public void testForEachIntegerSetWithIndex()
		{
			compareResourceEOE("loops/forEachIntegerSetWithIndex.e");
		}

		[Test]
		public void testForEachTimeRange()
		{
			compareResourceEOE("loops/forEachTimeRange.e");
		}

		[Test]
		public void testForEachTimeRangeWithIndex()
		{
			compareResourceEOE("loops/forEachTimeRangeWithIndex.e");
		}

		[Test]
		public void testForEachTupleList()
		{
			compareResourceEOE("loops/forEachTupleList.e");
		}

		[Test]
		public void testForEachTupleListWithIndex()
		{
			compareResourceEOE("loops/forEachTupleListWithIndex.e");
		}

		[Test]
		public void testForEachTupleSet()
		{
			compareResourceEOE("loops/forEachTupleSet.e");
		}

		[Test]
		public void testForEachTupleSetWithIndex()
		{
			compareResourceEOE("loops/forEachTupleSetWithIndex.e");
		}

		[Test]
		public void testWhile()
		{
			compareResourceEOE("loops/while.e");
		}

	}
}

