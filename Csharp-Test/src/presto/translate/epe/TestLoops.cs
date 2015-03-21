using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestLoops : BaseEParserTest
	{

		[Test]
		public void testDoWhile()
		{
			compareResourceEPE("loops/doWhile.e");
		}

		[Test]
		public void testForEachCharacterRange()
		{
			compareResourceEPE("loops/forEachCharacterRange.e");
		}

		[Test]
		public void testForEachCharacterRangeWithIndex()
		{
			compareResourceEPE("loops/forEachCharacterRangeWithIndex.e");
		}

		[Test]
		public void testForEachDateRange()
		{
			compareResourceEPE("loops/forEachDateRange.e");
		}

		[Test]
		public void testForEachDateRangeWithIndex()
		{
			compareResourceEPE("loops/forEachDateRangeWithIndex.e");
		}

		[Test]
		public void testForEachDictionaryItem()
		{
			compareResourceEPE("loops/forEachDictionaryItem.e");
		}

		[Test]
		public void testForEachDictionaryItemWithIndex()
		{
			compareResourceEPE("loops/forEachDictionaryItemWithIndex.e");
		}

		[Test]
		public void testForEachDictionaryKey()
		{
			compareResourceEPE("loops/forEachDictionaryKey.e");
		}

		[Test]
		public void testForEachDictionaryKeyWithIndex()
		{
			compareResourceEPE("loops/forEachDictionaryKeyWithIndex.e");
		}

		[Test]
		public void testForEachDictionaryValue()
		{
			compareResourceEPE("loops/forEachDictionaryValue.e");
		}

		[Test]
		public void testForEachDictionaryValueWithIndex()
		{
			compareResourceEPE("loops/forEachDictionaryValueWithIndex.e");
		}

		[Test]
		public void testForEachInstanceList()
		{
			compareResourceEPE("loops/forEachInstanceList.e");
		}

		[Test]
		public void testForEachInstanceListWithIndex()
		{
			compareResourceEPE("loops/forEachInstanceListWithIndex.e");
		}

		[Test]
		public void testForEachInstanceSet()
		{
			compareResourceEPE("loops/forEachInstanceSet.e");
		}

		[Test]
		public void testForEachInstanceSetWithIndex()
		{
			compareResourceEPE("loops/forEachInstanceSetWithIndex.e");
		}

		[Test]
		public void testForEachIntegerList()
		{
			compareResourceEPE("loops/forEachIntegerList.e");
		}

		[Test]
		public void testForEachIntegerListWithIndex()
		{
			compareResourceEPE("loops/forEachIntegerListWithIndex.e");
		}

		[Test]
		public void testForEachIntegerRange()
		{
			compareResourceEPE("loops/forEachIntegerRange.e");
		}

		[Test]
		public void testForEachIntegerRangeWithIndex()
		{
			compareResourceEPE("loops/forEachIntegerRangeWithIndex.e");
		}

		[Test]
		public void testForEachIntegerSet()
		{
			compareResourceEPE("loops/forEachIntegerSet.e");
		}

		[Test]
		public void testForEachIntegerSetWithIndex()
		{
			compareResourceEPE("loops/forEachIntegerSetWithIndex.e");
		}

		[Test]
		public void testForEachTimeRange()
		{
			compareResourceEPE("loops/forEachTimeRange.e");
		}

		[Test]
		public void testForEachTimeRangeWithIndex()
		{
			compareResourceEPE("loops/forEachTimeRangeWithIndex.e");
		}

		[Test]
		public void testForEachTupleList()
		{
			compareResourceEPE("loops/forEachTupleList.e");
		}

		[Test]
		public void testForEachTupleListWithIndex()
		{
			compareResourceEPE("loops/forEachTupleListWithIndex.e");
		}

		[Test]
		public void testForEachTupleSet()
		{
			compareResourceEPE("loops/forEachTupleSet.e");
		}

		[Test]
		public void testForEachTupleSetWithIndex()
		{
			compareResourceEPE("loops/forEachTupleSetWithIndex.e");
		}

		[Test]
		public void testWhile()
		{
			compareResourceEPE("loops/while.e");
		}

	}
}

