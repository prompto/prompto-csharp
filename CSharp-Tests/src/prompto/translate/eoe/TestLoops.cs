using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestLoops : BaseEParserTest
	{

		[Test]
		public void testDoWhile()
		{
			compareResourceEOE("loops/doWhile.pec");
		}

		[Test]
		public void testEmbeddedForEach()
		{
			compareResourceEOE("loops/embeddedForEach.pec");
		}

		[Test]
		public void testForEachCharacterRange()
		{
			compareResourceEOE("loops/forEachCharacterRange.pec");
		}

		[Test]
		public void testForEachCharacterRangeWithIndex()
		{
			compareResourceEOE("loops/forEachCharacterRangeWithIndex.pec");
		}

		[Test]
		public void testForEachDateRange()
		{
			compareResourceEOE("loops/forEachDateRange.pec");
		}

		[Test]
		public void testForEachDateRangeWithIndex()
		{
			compareResourceEOE("loops/forEachDateRangeWithIndex.pec");
		}

		[Test]
		public void testForEachDictionaryItem()
		{
			compareResourceEOE("loops/forEachDictionaryItem.pec");
		}

		[Test]
		public void testForEachDictionaryItemWithIndex()
		{
			compareResourceEOE("loops/forEachDictionaryItemWithIndex.pec");
		}

		[Test]
		public void testForEachDictionaryKey()
		{
			compareResourceEOE("loops/forEachDictionaryKey.pec");
		}

		[Test]
		public void testForEachDictionaryKeyWithIndex()
		{
			compareResourceEOE("loops/forEachDictionaryKeyWithIndex.pec");
		}

		[Test]
		public void testForEachDictionaryValue()
		{
			compareResourceEOE("loops/forEachDictionaryValue.pec");
		}

		[Test]
		public void testForEachDictionaryValueWithIndex()
		{
			compareResourceEOE("loops/forEachDictionaryValueWithIndex.pec");
		}

		[Test]
		public void testForEachInstanceList()
		{
			compareResourceEOE("loops/forEachInstanceList.pec");
		}

		[Test]
		public void testForEachInstanceListWithIndex()
		{
			compareResourceEOE("loops/forEachInstanceListWithIndex.pec");
		}

		[Test]
		public void testForEachInstanceSet()
		{
			compareResourceEOE("loops/forEachInstanceSet.pec");
		}

		[Test]
		public void testForEachInstanceSetWithIndex()
		{
			compareResourceEOE("loops/forEachInstanceSetWithIndex.pec");
		}

		[Test]
		public void testForEachIntegerList()
		{
			compareResourceEOE("loops/forEachIntegerList.pec");
		}

		[Test]
		public void testForEachIntegerListWithIndex()
		{
			compareResourceEOE("loops/forEachIntegerListWithIndex.pec");
		}

		[Test]
		public void testForEachIntegerRange()
		{
			compareResourceEOE("loops/forEachIntegerRange.pec");
		}

		[Test]
		public void testForEachIntegerRangeWithIndex()
		{
			compareResourceEOE("loops/forEachIntegerRangeWithIndex.pec");
		}

		[Test]
		public void testForEachIntegerSet()
		{
			compareResourceEOE("loops/forEachIntegerSet.pec");
		}

		[Test]
		public void testForEachIntegerSetWithIndex()
		{
			compareResourceEOE("loops/forEachIntegerSetWithIndex.pec");
		}

		[Test]
		public void testForEachTimeRange()
		{
			compareResourceEOE("loops/forEachTimeRange.pec");
		}

		[Test]
		public void testForEachTimeRangeWithIndex()
		{
			compareResourceEOE("loops/forEachTimeRangeWithIndex.pec");
		}

		[Test]
		public void testForEachTupleList()
		{
			compareResourceEOE("loops/forEachTupleList.pec");
		}

		[Test]
		public void testForEachTupleListWithIndex()
		{
			compareResourceEOE("loops/forEachTupleListWithIndex.pec");
		}

		[Test]
		public void testForEachTupleSet()
		{
			compareResourceEOE("loops/forEachTupleSet.pec");
		}

		[Test]
		public void testForEachTupleSetWithIndex()
		{
			compareResourceEOE("loops/forEachTupleSetWithIndex.pec");
		}

		[Test]
		public void testWhile()
		{
			compareResourceEOE("loops/while.pec");
		}

	}
}

