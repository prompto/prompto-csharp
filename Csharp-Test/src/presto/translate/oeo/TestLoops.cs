using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestLoops : BaseOParserTest
	{

		[Test]
		public void testDoWhile()
		{
			compareResourceOEO("loops/doWhile.o");
		}

		[Test]
		public void testForEachCharacterRange()
		{
			compareResourceOEO("loops/forEachCharacterRange.o");
		}

		[Test]
		public void testForEachCharacterRangeWithIndex()
		{
			compareResourceOEO("loops/forEachCharacterRangeWithIndex.o");
		}

		[Test]
		public void testForEachDateRange()
		{
			compareResourceOEO("loops/forEachDateRange.o");
		}

		[Test]
		public void testForEachDateRangeWithIndex()
		{
			compareResourceOEO("loops/forEachDateRangeWithIndex.o");
		}

		[Test]
		public void testForEachDictionaryItem()
		{
			compareResourceOEO("loops/forEachDictionaryItem.o");
		}

		[Test]
		public void testForEachDictionaryItemWithIndex()
		{
			compareResourceOEO("loops/forEachDictionaryItemWithIndex.o");
		}

		[Test]
		public void testForEachDictionaryKey()
		{
			compareResourceOEO("loops/forEachDictionaryKey.o");
		}

		[Test]
		public void testForEachDictionaryKeyWithIndex()
		{
			compareResourceOEO("loops/forEachDictionaryKeyWithIndex.o");
		}

		[Test]
		public void testForEachDictionaryValue()
		{
			compareResourceOEO("loops/forEachDictionaryValue.o");
		}

		[Test]
		public void testForEachDictionaryValueWithIndex()
		{
			compareResourceOEO("loops/forEachDictionaryValueWithIndex.o");
		}

		[Test]
		public void testForEachInstanceList()
		{
			compareResourceOEO("loops/forEachInstanceList.o");
		}

		[Test]
		public void testForEachInstanceListWithIndex()
		{
			compareResourceOEO("loops/forEachInstanceListWithIndex.o");
		}

		[Test]
		public void testForEachInstanceSet()
		{
			compareResourceOEO("loops/forEachInstanceSet.o");
		}

		[Test]
		public void testForEachInstanceSetWithIndex()
		{
			compareResourceOEO("loops/forEachInstanceSetWithIndex.o");
		}

		[Test]
		public void testForEachIntegerList()
		{
			compareResourceOEO("loops/forEachIntegerList.o");
		}

		[Test]
		public void testForEachIntegerListWithIndex()
		{
			compareResourceOEO("loops/forEachIntegerListWithIndex.o");
		}

		[Test]
		public void testForEachIntegerRange()
		{
			compareResourceOEO("loops/forEachIntegerRange.o");
		}

		[Test]
		public void testForEachIntegerRangeWithIndex()
		{
			compareResourceOEO("loops/forEachIntegerRangeWithIndex.o");
		}

		[Test]
		public void testForEachIntegerSet()
		{
			compareResourceOEO("loops/forEachIntegerSet.o");
		}

		[Test]
		public void testForEachIntegerSetWithIndex()
		{
			compareResourceOEO("loops/forEachIntegerSetWithIndex.o");
		}

		[Test]
		public void testForEachTimeRange()
		{
			compareResourceOEO("loops/forEachTimeRange.o");
		}

		[Test]
		public void testForEachTimeRangeWithIndex()
		{
			compareResourceOEO("loops/forEachTimeRangeWithIndex.o");
		}

		[Test]
		public void testForEachTupleList()
		{
			compareResourceOEO("loops/forEachTupleList.o");
		}

		[Test]
		public void testForEachTupleListWithIndex()
		{
			compareResourceOEO("loops/forEachTupleListWithIndex.o");
		}

		[Test]
		public void testForEachTupleSet()
		{
			compareResourceOEO("loops/forEachTupleSet.o");
		}

		[Test]
		public void testForEachTupleSetWithIndex()
		{
			compareResourceOEO("loops/forEachTupleSetWithIndex.o");
		}

		[Test]
		public void testWhile()
		{
			compareResourceOEO("loops/while.o");
		}

	}
}

