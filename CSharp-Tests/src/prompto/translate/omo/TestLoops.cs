using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.omo
{

	[TestFixture]
	public class TestLoops : BaseOParserTest
	{

		[Test]
		public void testDoWhile()
		{
			compareResourceOMO("loops/doWhile.poc");
		}

		[Test]
		public void testDoWhileBreak()
		{
			compareResourceOMO("loops/doWhileBreak.poc");
		}

		[Test]
		public void testEmbeddedForEach()
		{
			compareResourceOMO("loops/embeddedForEach.poc");
		}

		[Test]
		public void testForEachBreak()
		{
			compareResourceOMO("loops/forEachBreak.poc");
		}

		[Test]
		public void testForEachCharacterRange()
		{
			compareResourceOMO("loops/forEachCharacterRange.poc");
		}

		[Test]
		public void testForEachCharacterRangeWithIndex()
		{
			compareResourceOMO("loops/forEachCharacterRangeWithIndex.poc");
		}

		[Test]
		public void testForEachDateRange()
		{
			compareResourceOMO("loops/forEachDateRange.poc");
		}

		[Test]
		public void testForEachDateRangeWithIndex()
		{
			compareResourceOMO("loops/forEachDateRangeWithIndex.poc");
		}

		[Test]
		public void testForEachDictionaryItem()
		{
			compareResourceOMO("loops/forEachDictionaryItem.poc");
		}

		[Test]
		public void testForEachDictionaryItemWithIndex()
		{
			compareResourceOMO("loops/forEachDictionaryItemWithIndex.poc");
		}

		[Test]
		public void testForEachDictionaryKey()
		{
			compareResourceOMO("loops/forEachDictionaryKey.poc");
		}

		[Test]
		public void testForEachDictionaryKeyWithIndex()
		{
			compareResourceOMO("loops/forEachDictionaryKeyWithIndex.poc");
		}

		[Test]
		public void testForEachDictionaryValue()
		{
			compareResourceOMO("loops/forEachDictionaryValue.poc");
		}

		[Test]
		public void testForEachDictionaryValueWithIndex()
		{
			compareResourceOMO("loops/forEachDictionaryValueWithIndex.poc");
		}

		[Test]
		public void testForEachInstanceList()
		{
			compareResourceOMO("loops/forEachInstanceList.poc");
		}

		[Test]
		public void testForEachInstanceListWithIndex()
		{
			compareResourceOMO("loops/forEachInstanceListWithIndex.poc");
		}

		[Test]
		public void testForEachInstanceSet()
		{
			compareResourceOMO("loops/forEachInstanceSet.poc");
		}

		[Test]
		public void testForEachInstanceSetWithIndex()
		{
			compareResourceOMO("loops/forEachInstanceSetWithIndex.poc");
		}

		[Test]
		public void testForEachIntegerList()
		{
			compareResourceOMO("loops/forEachIntegerList.poc");
		}

		[Test]
		public void testForEachIntegerListWithIndex()
		{
			compareResourceOMO("loops/forEachIntegerListWithIndex.poc");
		}

		[Test]
		public void testForEachIntegerRange()
		{
			compareResourceOMO("loops/forEachIntegerRange.poc");
		}

		[Test]
		public void testForEachIntegerRangeWithIndex()
		{
			compareResourceOMO("loops/forEachIntegerRangeWithIndex.poc");
		}

		[Test]
		public void testForEachIntegerSet()
		{
			compareResourceOMO("loops/forEachIntegerSet.poc");
		}

		[Test]
		public void testForEachIntegerSetWithIndex()
		{
			compareResourceOMO("loops/forEachIntegerSetWithIndex.poc");
		}

		[Test]
		public void testForEachTimeRange()
		{
			compareResourceOMO("loops/forEachTimeRange.poc");
		}

		[Test]
		public void testForEachTimeRangeWithIndex()
		{
			compareResourceOMO("loops/forEachTimeRangeWithIndex.poc");
		}

		[Test]
		public void testForEachTupleList()
		{
			compareResourceOMO("loops/forEachTupleList.poc");
		}

		[Test]
		public void testForEachTupleListWithIndex()
		{
			compareResourceOMO("loops/forEachTupleListWithIndex.poc");
		}

		[Test]
		public void testForEachTupleSet()
		{
			compareResourceOMO("loops/forEachTupleSet.poc");
		}

		[Test]
		public void testForEachTupleSetWithIndex()
		{
			compareResourceOMO("loops/forEachTupleSetWithIndex.poc");
		}

		[Test]
		public void testWhile()
		{
			compareResourceOMO("loops/while.poc");
		}

		[Test]
		public void testWhileBreak()
		{
			compareResourceOMO("loops/whileBreak.poc");
		}

	}
}

