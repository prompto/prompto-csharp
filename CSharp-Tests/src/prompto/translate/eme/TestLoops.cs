using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestLoops : BaseEParserTest
	{

		[Test]
		public void testDoWhile()
		{
			compareResourceEME("loops/doWhile.pec");
		}

		[Test]
		public void testDoWhileBreak()
		{
			compareResourceEME("loops/doWhileBreak.pec");
		}

		[Test]
		public void testEmbeddedForEach()
		{
			compareResourceEME("loops/embeddedForEach.pec");
		}

		[Test]
		public void testForEachBreak()
		{
			compareResourceEME("loops/forEachBreak.pec");
		}

		[Test]
		public void testForEachCharacterRange()
		{
			compareResourceEME("loops/forEachCharacterRange.pec");
		}

		[Test]
		public void testForEachCharacterRangeWithIndex()
		{
			compareResourceEME("loops/forEachCharacterRangeWithIndex.pec");
		}

		[Test]
		public void testForEachDateRange()
		{
			compareResourceEME("loops/forEachDateRange.pec");
		}

		[Test]
		public void testForEachDateRangeWithIndex()
		{
			compareResourceEME("loops/forEachDateRangeWithIndex.pec");
		}

		[Test]
		public void testForEachDictionaryItem()
		{
			compareResourceEME("loops/forEachDictionaryItem.pec");
		}

		[Test]
		public void testForEachDictionaryItemWithIndex()
		{
			compareResourceEME("loops/forEachDictionaryItemWithIndex.pec");
		}

		[Test]
		public void testForEachDictionaryKey()
		{
			compareResourceEME("loops/forEachDictionaryKey.pec");
		}

		[Test]
		public void testForEachDictionaryKeyWithIndex()
		{
			compareResourceEME("loops/forEachDictionaryKeyWithIndex.pec");
		}

		[Test]
		public void testForEachDictionaryValue()
		{
			compareResourceEME("loops/forEachDictionaryValue.pec");
		}

		[Test]
		public void testForEachDictionaryValueWithIndex()
		{
			compareResourceEME("loops/forEachDictionaryValueWithIndex.pec");
		}

		[Test]
		public void testForEachInstanceList()
		{
			compareResourceEME("loops/forEachInstanceList.pec");
		}

		[Test]
		public void testForEachInstanceListWithIndex()
		{
			compareResourceEME("loops/forEachInstanceListWithIndex.pec");
		}

		[Test]
		public void testForEachInstanceSet()
		{
			compareResourceEME("loops/forEachInstanceSet.pec");
		}

		[Test]
		public void testForEachInstanceSetWithIndex()
		{
			compareResourceEME("loops/forEachInstanceSetWithIndex.pec");
		}

		[Test]
		public void testForEachIntegerList()
		{
			compareResourceEME("loops/forEachIntegerList.pec");
		}

		[Test]
		public void testForEachIntegerListWithIndex()
		{
			compareResourceEME("loops/forEachIntegerListWithIndex.pec");
		}

		[Test]
		public void testForEachIntegerRange()
		{
			compareResourceEME("loops/forEachIntegerRange.pec");
		}

		[Test]
		public void testForEachIntegerRangeWithIndex()
		{
			compareResourceEME("loops/forEachIntegerRangeWithIndex.pec");
		}

		[Test]
		public void testForEachIntegerSet()
		{
			compareResourceEME("loops/forEachIntegerSet.pec");
		}

		[Test]
		public void testForEachIntegerSetWithIndex()
		{
			compareResourceEME("loops/forEachIntegerSetWithIndex.pec");
		}

		[Test]
		public void testForEachTimeRange()
		{
			compareResourceEME("loops/forEachTimeRange.pec");
		}

		[Test]
		public void testForEachTimeRangeWithIndex()
		{
			compareResourceEME("loops/forEachTimeRangeWithIndex.pec");
		}

		[Test]
		public void testForEachTupleList()
		{
			compareResourceEME("loops/forEachTupleList.pec");
		}

		[Test]
		public void testForEachTupleListWithIndex()
		{
			compareResourceEME("loops/forEachTupleListWithIndex.pec");
		}

		[Test]
		public void testForEachTupleSet()
		{
			compareResourceEME("loops/forEachTupleSet.pec");
		}

		[Test]
		public void testForEachTupleSetWithIndex()
		{
			compareResourceEME("loops/forEachTupleSetWithIndex.pec");
		}

		[Test]
		public void testWhile()
		{
			compareResourceEME("loops/while.pec");
		}

		[Test]
		public void testWhileBreak()
		{
			compareResourceEME("loops/whileBreak.pec");
		}

	}
}

