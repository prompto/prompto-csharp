using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
{

	[TestFixture]
	public class TestLoops : BaseOParserTest
	{

		[Test]
		public void testDoWhile()
		{
			compareResourceOEO("loops/doWhile.poc");
		}

		[Test]
		public void testDoWhileBreak()
		{
			compareResourceOEO("loops/doWhileBreak.poc");
		}

		[Test]
		public void testEmbeddedForEach()
		{
			compareResourceOEO("loops/embeddedForEach.poc");
		}

		[Test]
		public void testForEachBreak()
		{
			compareResourceOEO("loops/forEachBreak.poc");
		}

		[Test]
		public void testForEachCharacterRange()
		{
			compareResourceOEO("loops/forEachCharacterRange.poc");
		}

		[Test]
		public void testForEachCharacterRangeWithIndex()
		{
			compareResourceOEO("loops/forEachCharacterRangeWithIndex.poc");
		}

		[Test]
		public void testForEachDateRange()
		{
			compareResourceOEO("loops/forEachDateRange.poc");
		}

		[Test]
		public void testForEachDateRangeWithIndex()
		{
			compareResourceOEO("loops/forEachDateRangeWithIndex.poc");
		}

		[Test]
		public void testForEachDictionaryItem()
		{
			compareResourceOEO("loops/forEachDictionaryItem.poc");
		}

		[Test]
		public void testForEachDictionaryItemWithIndex()
		{
			compareResourceOEO("loops/forEachDictionaryItemWithIndex.poc");
		}

		[Test]
		public void testForEachDictionaryKey()
		{
			compareResourceOEO("loops/forEachDictionaryKey.poc");
		}

		[Test]
		public void testForEachDictionaryKeyWithIndex()
		{
			compareResourceOEO("loops/forEachDictionaryKeyWithIndex.poc");
		}

		[Test]
		public void testForEachDictionaryValue()
		{
			compareResourceOEO("loops/forEachDictionaryValue.poc");
		}

		[Test]
		public void testForEachDictionaryValueWithIndex()
		{
			compareResourceOEO("loops/forEachDictionaryValueWithIndex.poc");
		}

		[Test]
		public void testForEachInstanceList()
		{
			compareResourceOEO("loops/forEachInstanceList.poc");
		}

		[Test]
		public void testForEachInstanceListWithIndex()
		{
			compareResourceOEO("loops/forEachInstanceListWithIndex.poc");
		}

		[Test]
		public void testForEachInstanceSet()
		{
			compareResourceOEO("loops/forEachInstanceSet.poc");
		}

		[Test]
		public void testForEachInstanceSetWithIndex()
		{
			compareResourceOEO("loops/forEachInstanceSetWithIndex.poc");
		}

		[Test]
		public void testForEachIntegerList()
		{
			compareResourceOEO("loops/forEachIntegerList.poc");
		}

		[Test]
		public void testForEachIntegerListWithIndex()
		{
			compareResourceOEO("loops/forEachIntegerListWithIndex.poc");
		}

		[Test]
		public void testForEachIntegerRange()
		{
			compareResourceOEO("loops/forEachIntegerRange.poc");
		}

		[Test]
		public void testForEachIntegerRangeWithIndex()
		{
			compareResourceOEO("loops/forEachIntegerRangeWithIndex.poc");
		}

		[Test]
		public void testForEachIntegerSet()
		{
			compareResourceOEO("loops/forEachIntegerSet.poc");
		}

		[Test]
		public void testForEachIntegerSetWithIndex()
		{
			compareResourceOEO("loops/forEachIntegerSetWithIndex.poc");
		}

		[Test]
		public void testForEachTextCharacter()
		{
			compareResourceOEO("loops/forEachTextCharacter.poc");
		}

		[Test]
		public void testForEachTimeRange()
		{
			compareResourceOEO("loops/forEachTimeRange.poc");
		}

		[Test]
		public void testForEachTimeRangeWithIndex()
		{
			compareResourceOEO("loops/forEachTimeRangeWithIndex.poc");
		}

		[Test]
		public void testForEachTupleList()
		{
			compareResourceOEO("loops/forEachTupleList.poc");
		}

		[Test]
		public void testForEachTupleListWithIndex()
		{
			compareResourceOEO("loops/forEachTupleListWithIndex.poc");
		}

		[Test]
		public void testForEachTupleSet()
		{
			compareResourceOEO("loops/forEachTupleSet.poc");
		}

		[Test]
		public void testForEachTupleSetWithIndex()
		{
			compareResourceOEO("loops/forEachTupleSetWithIndex.poc");
		}

		[Test]
		public void testWhile()
		{
			compareResourceOEO("loops/while.poc");
		}

		[Test]
		public void testWhileBreak()
		{
			compareResourceOEO("loops/whileBreak.poc");
		}

	}
}

