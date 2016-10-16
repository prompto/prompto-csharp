using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestLoops : BaseEParserTest
	{

		[Test]
		public void testDoWhile()
		{
			compareResourceESE("loops/doWhile.pec");
		}

		[Test]
		public void testDoWhileBreak()
		{
			compareResourceESE("loops/doWhileBreak.pec");
		}

		[Test]
		public void testEmbeddedForEach()
		{
			compareResourceESE("loops/embeddedForEach.pec");
		}

		[Test]
		public void testForEachBreak()
		{
			compareResourceESE("loops/forEachBreak.pec");
		}

		[Test]
		public void testForEachCharacterRange()
		{
			compareResourceESE("loops/forEachCharacterRange.pec");
		}

		[Test]
		public void testForEachCharacterRangeWithIndex()
		{
			compareResourceESE("loops/forEachCharacterRangeWithIndex.pec");
		}

		[Test]
		public void testForEachDateRange()
		{
			compareResourceESE("loops/forEachDateRange.pec");
		}

		[Test]
		public void testForEachDateRangeWithIndex()
		{
			compareResourceESE("loops/forEachDateRangeWithIndex.pec");
		}

		[Test]
		public void testForEachDictionaryItem()
		{
			compareResourceESE("loops/forEachDictionaryItem.pec");
		}

		[Test]
		public void testForEachDictionaryItemWithIndex()
		{
			compareResourceESE("loops/forEachDictionaryItemWithIndex.pec");
		}

		[Test]
		public void testForEachDictionaryKey()
		{
			compareResourceESE("loops/forEachDictionaryKey.pec");
		}

		[Test]
		public void testForEachDictionaryKeyWithIndex()
		{
			compareResourceESE("loops/forEachDictionaryKeyWithIndex.pec");
		}

		[Test]
		public void testForEachDictionaryValue()
		{
			compareResourceESE("loops/forEachDictionaryValue.pec");
		}

		[Test]
		public void testForEachDictionaryValueWithIndex()
		{
			compareResourceESE("loops/forEachDictionaryValueWithIndex.pec");
		}

		[Test]
		public void testForEachInstanceList()
		{
			compareResourceESE("loops/forEachInstanceList.pec");
		}

		[Test]
		public void testForEachInstanceListWithIndex()
		{
			compareResourceESE("loops/forEachInstanceListWithIndex.pec");
		}

		[Test]
		public void testForEachInstanceSet()
		{
			compareResourceESE("loops/forEachInstanceSet.pec");
		}

		[Test]
		public void testForEachInstanceSetWithIndex()
		{
			compareResourceESE("loops/forEachInstanceSetWithIndex.pec");
		}

		[Test]
		public void testForEachIntegerList()
		{
			compareResourceESE("loops/forEachIntegerList.pec");
		}

		[Test]
		public void testForEachIntegerListWithIndex()
		{
			compareResourceESE("loops/forEachIntegerListWithIndex.pec");
		}

		[Test]
		public void testForEachIntegerRange()
		{
			compareResourceESE("loops/forEachIntegerRange.pec");
		}

		[Test]
		public void testForEachIntegerRangeWithIndex()
		{
			compareResourceESE("loops/forEachIntegerRangeWithIndex.pec");
		}

		[Test]
		public void testForEachIntegerSet()
		{
			compareResourceESE("loops/forEachIntegerSet.pec");
		}

		[Test]
		public void testForEachIntegerSetWithIndex()
		{
			compareResourceESE("loops/forEachIntegerSetWithIndex.pec");
		}

		[Test]
		public void testForEachTimeRange()
		{
			compareResourceESE("loops/forEachTimeRange.pec");
		}

		[Test]
		public void testForEachTimeRangeWithIndex()
		{
			compareResourceESE("loops/forEachTimeRangeWithIndex.pec");
		}

		[Test]
		public void testForEachTupleList()
		{
			compareResourceESE("loops/forEachTupleList.pec");
		}

		[Test]
		public void testForEachTupleListWithIndex()
		{
			compareResourceESE("loops/forEachTupleListWithIndex.pec");
		}

		[Test]
		public void testForEachTupleSet()
		{
			compareResourceESE("loops/forEachTupleSet.pec");
		}

		[Test]
		public void testForEachTupleSetWithIndex()
		{
			compareResourceESE("loops/forEachTupleSetWithIndex.pec");
		}

		[Test]
		public void testWhile()
		{
			compareResourceESE("loops/while.pec");
		}

		[Test]
		public void testWhileBreak()
		{
			compareResourceESE("loops/whileBreak.pec");
		}

	}
}

