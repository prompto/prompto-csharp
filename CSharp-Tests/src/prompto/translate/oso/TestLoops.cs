// generated: 2015-07-05T23:01:01.321
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
{

	[TestFixture]
	public class TestLoops : BaseOParserTest
	{

		[Test]
		public void testDoWhile()
		{
			compareResourceOSO("loops/doWhile.poc");
		}

		[Test]
		public void testForEachCharacterRange()
		{
			compareResourceOSO("loops/forEachCharacterRange.poc");
		}

		[Test]
		public void testForEachCharacterRangeWithIndex()
		{
			compareResourceOSO("loops/forEachCharacterRangeWithIndex.poc");
		}

		[Test]
		public void testForEachDateRange()
		{
			compareResourceOSO("loops/forEachDateRange.poc");
		}

		[Test]
		public void testForEachDateRangeWithIndex()
		{
			compareResourceOSO("loops/forEachDateRangeWithIndex.poc");
		}

		[Test]
		public void testForEachDictionaryItem()
		{
			compareResourceOSO("loops/forEachDictionaryItem.poc");
		}

		[Test]
		public void testForEachDictionaryItemWithIndex()
		{
			compareResourceOSO("loops/forEachDictionaryItemWithIndex.poc");
		}

		[Test]
		public void testForEachDictionaryKey()
		{
			compareResourceOSO("loops/forEachDictionaryKey.poc");
		}

		[Test]
		public void testForEachDictionaryKeyWithIndex()
		{
			compareResourceOSO("loops/forEachDictionaryKeyWithIndex.poc");
		}

		[Test]
		public void testForEachDictionaryValue()
		{
			compareResourceOSO("loops/forEachDictionaryValue.poc");
		}

		[Test]
		public void testForEachDictionaryValueWithIndex()
		{
			compareResourceOSO("loops/forEachDictionaryValueWithIndex.poc");
		}

		[Test]
		public void testForEachInstanceList()
		{
			compareResourceOSO("loops/forEachInstanceList.poc");
		}

		[Test]
		public void testForEachInstanceListWithIndex()
		{
			compareResourceOSO("loops/forEachInstanceListWithIndex.poc");
		}

		[Test]
		public void testForEachInstanceSet()
		{
			compareResourceOSO("loops/forEachInstanceSet.poc");
		}

		[Test]
		public void testForEachInstanceSetWithIndex()
		{
			compareResourceOSO("loops/forEachInstanceSetWithIndex.poc");
		}

		[Test]
		public void testForEachIntegerList()
		{
			compareResourceOSO("loops/forEachIntegerList.poc");
		}

		[Test]
		public void testForEachIntegerListWithIndex()
		{
			compareResourceOSO("loops/forEachIntegerListWithIndex.poc");
		}

		[Test]
		public void testForEachIntegerRange()
		{
			compareResourceOSO("loops/forEachIntegerRange.poc");
		}

		[Test]
		public void testForEachIntegerRangeWithIndex()
		{
			compareResourceOSO("loops/forEachIntegerRangeWithIndex.poc");
		}

		[Test]
		public void testForEachIntegerSet()
		{
			compareResourceOSO("loops/forEachIntegerSet.poc");
		}

		[Test]
		public void testForEachIntegerSetWithIndex()
		{
			compareResourceOSO("loops/forEachIntegerSetWithIndex.poc");
		}

		[Test]
		public void testForEachTimeRange()
		{
			compareResourceOSO("loops/forEachTimeRange.poc");
		}

		[Test]
		public void testForEachTimeRangeWithIndex()
		{
			compareResourceOSO("loops/forEachTimeRangeWithIndex.poc");
		}

		[Test]
		public void testForEachTupleList()
		{
			compareResourceOSO("loops/forEachTupleList.poc");
		}

		[Test]
		public void testForEachTupleListWithIndex()
		{
			compareResourceOSO("loops/forEachTupleListWithIndex.poc");
		}

		[Test]
		public void testForEachTupleSet()
		{
			compareResourceOSO("loops/forEachTupleSet.poc");
		}

		[Test]
		public void testForEachTupleSetWithIndex()
		{
			compareResourceOSO("loops/forEachTupleSetWithIndex.poc");
		}

		[Test]
		public void testWhile()
		{
			compareResourceOSO("loops/while.poc");
		}

	}
}

