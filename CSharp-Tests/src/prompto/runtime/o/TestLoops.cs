using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.o
{

	[TestFixture]
	public class TestLoops : BaseOParserTest
	{

		[SetUp]
		public void before()
		{
			Out.init();
		}

		[TearDown]
		public void after()
		{
			Out.restore();
		}

		[Test]
		public void testDoWhile()
		{
			CheckOutput("loops/doWhile.poc");
		}

		[Test]
		public void testDoWhileBreak()
		{
			CheckOutput("loops/doWhileBreak.poc");
		}

		[Test]
		public void testEmbeddedForEach()
		{
			CheckOutput("loops/embeddedForEach.poc");
		}

		[Test]
		public void testForEachBreak()
		{
			CheckOutput("loops/forEachBreak.poc");
		}

		[Test]
		public void testForEachCharacterRange()
		{
			CheckOutput("loops/forEachCharacterRange.poc");
		}

		[Test]
		public void testForEachCharacterRangeWithIndex()
		{
			CheckOutput("loops/forEachCharacterRangeWithIndex.poc");
		}

		[Test]
		public void testForEachDateRange()
		{
			CheckOutput("loops/forEachDateRange.poc");
		}

		[Test]
		public void testForEachDateRangeWithIndex()
		{
			CheckOutput("loops/forEachDateRangeWithIndex.poc");
		}

		[Test]
		public void testForEachDictionaryItem()
		{
			CheckOutput("loops/forEachDictionaryItem.poc");
		}

		[Test]
		public void testForEachDictionaryItemWithIndex()
		{
			CheckOutput("loops/forEachDictionaryItemWithIndex.poc");
		}

		[Test]
		public void testForEachDictionaryKey()
		{
			CheckOutput("loops/forEachDictionaryKey.poc");
		}

		[Test]
		public void testForEachDictionaryKeyWithIndex()
		{
			CheckOutput("loops/forEachDictionaryKeyWithIndex.poc");
		}

		[Test]
		public void testForEachDictionaryValue()
		{
			CheckOutput("loops/forEachDictionaryValue.poc");
		}

		[Test]
		public void testForEachDictionaryValueWithIndex()
		{
			CheckOutput("loops/forEachDictionaryValueWithIndex.poc");
		}

		[Test]
		public void testForEachInstanceList()
		{
			CheckOutput("loops/forEachInstanceList.poc");
		}

		[Test]
		public void testForEachInstanceListWithIndex()
		{
			CheckOutput("loops/forEachInstanceListWithIndex.poc");
		}

		[Test]
		public void testForEachInstanceSet()
		{
			CheckOutput("loops/forEachInstanceSet.poc");
		}

		[Test]
		public void testForEachInstanceSetWithIndex()
		{
			CheckOutput("loops/forEachInstanceSetWithIndex.poc");
		}

		[Test]
		public void testForEachIntegerList()
		{
			CheckOutput("loops/forEachIntegerList.poc");
		}

		[Test]
		public void testForEachIntegerListWithIndex()
		{
			CheckOutput("loops/forEachIntegerListWithIndex.poc");
		}

		[Test]
		public void testForEachIntegerRange()
		{
			CheckOutput("loops/forEachIntegerRange.poc");
		}

		[Test]
		public void testForEachIntegerRangeWithIndex()
		{
			CheckOutput("loops/forEachIntegerRangeWithIndex.poc");
		}

		[Test]
		public void testForEachIntegerSet()
		{
			CheckOutput("loops/forEachIntegerSet.poc");
		}

		[Test]
		public void testForEachIntegerSetWithIndex()
		{
			CheckOutput("loops/forEachIntegerSetWithIndex.poc");
		}

		[Test]
		public void testForEachTimeRange()
		{
			CheckOutput("loops/forEachTimeRange.poc");
		}

		[Test]
		public void testForEachTimeRangeWithIndex()
		{
			CheckOutput("loops/forEachTimeRangeWithIndex.poc");
		}

		[Test]
		public void testForEachTupleList()
		{
			CheckOutput("loops/forEachTupleList.poc");
		}

		[Test]
		public void testForEachTupleListWithIndex()
		{
			CheckOutput("loops/forEachTupleListWithIndex.poc");
		}

		[Test]
		public void testForEachTupleSet()
		{
			CheckOutput("loops/forEachTupleSet.poc");
		}

		[Test]
		public void testForEachTupleSetWithIndex()
		{
			CheckOutput("loops/forEachTupleSetWithIndex.poc");
		}

		[Test]
		public void testWhile()
		{
			CheckOutput("loops/while.poc");
		}

		[Test]
		public void testWhileBreak()
		{
			CheckOutput("loops/whileBreak.poc");
		}

	}
}

