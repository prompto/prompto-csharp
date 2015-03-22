using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestLoops : BaseOParserTest
	{

		[Test]
		public void testDoWhile()
		{
			compareResourceOPO("loops/doWhile.o");
		}

		[Test]
		public void testForEachCharacterRange()
		{
			compareResourceOPO("loops/forEachCharacterRange.o");
		}

		[Test]
		public void testForEachCharacterRangeWithIndex()
		{
			compareResourceOPO("loops/forEachCharacterRangeWithIndex.o");
		}

		[Test]
		public void testForEachDateRange()
		{
			compareResourceOPO("loops/forEachDateRange.o");
		}

		[Test]
		public void testForEachDateRangeWithIndex()
		{
			compareResourceOPO("loops/forEachDateRangeWithIndex.o");
		}

		[Test]
		public void testForEachDictionaryItem()
		{
			compareResourceOPO("loops/forEachDictionaryItem.o");
		}

		[Test]
		public void testForEachDictionaryItemWithIndex()
		{
			compareResourceOPO("loops/forEachDictionaryItemWithIndex.o");
		}

		[Test]
		public void testForEachDictionaryKey()
		{
			compareResourceOPO("loops/forEachDictionaryKey.o");
		}

		[Test]
		public void testForEachDictionaryKeyWithIndex()
		{
			compareResourceOPO("loops/forEachDictionaryKeyWithIndex.o");
		}

		[Test]
		public void testForEachDictionaryValue()
		{
			compareResourceOPO("loops/forEachDictionaryValue.o");
		}

		[Test]
		public void testForEachDictionaryValueWithIndex()
		{
			compareResourceOPO("loops/forEachDictionaryValueWithIndex.o");
		}

		[Test]
		public void testForEachInstanceList()
		{
			compareResourceOPO("loops/forEachInstanceList.o");
		}

		[Test]
		public void testForEachInstanceListWithIndex()
		{
			compareResourceOPO("loops/forEachInstanceListWithIndex.o");
		}

		[Test]
		public void testForEachInstanceSet()
		{
			compareResourceOPO("loops/forEachInstanceSet.o");
		}

		[Test]
		public void testForEachInstanceSetWithIndex()
		{
			compareResourceOPO("loops/forEachInstanceSetWithIndex.o");
		}

		[Test]
		public void testForEachIntegerList()
		{
			compareResourceOPO("loops/forEachIntegerList.o");
		}

		[Test]
		public void testForEachIntegerListWithIndex()
		{
			compareResourceOPO("loops/forEachIntegerListWithIndex.o");
		}

		[Test]
		public void testForEachIntegerRange()
		{
			compareResourceOPO("loops/forEachIntegerRange.o");
		}

		[Test]
		public void testForEachIntegerRangeWithIndex()
		{
			compareResourceOPO("loops/forEachIntegerRangeWithIndex.o");
		}

		[Test]
		public void testForEachIntegerSet()
		{
			compareResourceOPO("loops/forEachIntegerSet.o");
		}

		[Test]
		public void testForEachIntegerSetWithIndex()
		{
			compareResourceOPO("loops/forEachIntegerSetWithIndex.o");
		}

		[Test]
		public void testForEachTimeRange()
		{
			compareResourceOPO("loops/forEachTimeRange.o");
		}

		[Test]
		public void testForEachTimeRangeWithIndex()
		{
			compareResourceOPO("loops/forEachTimeRangeWithIndex.o");
		}

		[Test]
		public void testForEachTupleList()
		{
			compareResourceOPO("loops/forEachTupleList.o");
		}

		[Test]
		public void testForEachTupleListWithIndex()
		{
			compareResourceOPO("loops/forEachTupleListWithIndex.o");
		}

		[Test]
		public void testForEachTupleSet()
		{
			compareResourceOPO("loops/forEachTupleSet.o");
		}

		[Test]
		public void testForEachTupleSetWithIndex()
		{
			compareResourceOPO("loops/forEachTupleSetWithIndex.o");
		}

		[Test]
		public void testWhile()
		{
			compareResourceOPO("loops/while.o");
		}

	}
}

