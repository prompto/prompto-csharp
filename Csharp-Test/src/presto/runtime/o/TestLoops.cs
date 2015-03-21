using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
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
			CheckOutput("loops/doWhile.o");
		}

		[Test]
		public void testForEachCharacterRange()
		{
			CheckOutput("loops/forEachCharacterRange.o");
		}

		[Test]
		public void testForEachCharacterRangeWithIndex()
		{
			CheckOutput("loops/forEachCharacterRangeWithIndex.o");
		}

		[Test]
		public void testForEachDateRange()
		{
			CheckOutput("loops/forEachDateRange.o");
		}

		[Test]
		public void testForEachDateRangeWithIndex()
		{
			CheckOutput("loops/forEachDateRangeWithIndex.o");
		}

		[Test]
		public void testForEachDictionaryItem()
		{
			CheckOutput("loops/forEachDictionaryItem.o");
		}

		[Test]
		public void testForEachDictionaryItemWithIndex()
		{
			CheckOutput("loops/forEachDictionaryItemWithIndex.o");
		}

		[Test]
		public void testForEachDictionaryKey()
		{
			CheckOutput("loops/forEachDictionaryKey.o");
		}

		[Test]
		public void testForEachDictionaryKeyWithIndex()
		{
			CheckOutput("loops/forEachDictionaryKeyWithIndex.o");
		}

		[Test]
		public void testForEachDictionaryValue()
		{
			CheckOutput("loops/forEachDictionaryValue.o");
		}

		[Test]
		public void testForEachDictionaryValueWithIndex()
		{
			CheckOutput("loops/forEachDictionaryValueWithIndex.o");
		}

		[Test]
		public void testForEachInstanceList()
		{
			CheckOutput("loops/forEachInstanceList.o");
		}

		[Test]
		public void testForEachInstanceListWithIndex()
		{
			CheckOutput("loops/forEachInstanceListWithIndex.o");
		}

		[Test]
		public void testForEachInstanceSet()
		{
			CheckOutput("loops/forEachInstanceSet.o");
		}

		[Test]
		public void testForEachInstanceSetWithIndex()
		{
			CheckOutput("loops/forEachInstanceSetWithIndex.o");
		}

		[Test]
		public void testForEachIntegerList()
		{
			CheckOutput("loops/forEachIntegerList.o");
		}

		[Test]
		public void testForEachIntegerListWithIndex()
		{
			CheckOutput("loops/forEachIntegerListWithIndex.o");
		}

		[Test]
		public void testForEachIntegerRange()
		{
			CheckOutput("loops/forEachIntegerRange.o");
		}

		[Test]
		public void testForEachIntegerRangeWithIndex()
		{
			CheckOutput("loops/forEachIntegerRangeWithIndex.o");
		}

		[Test]
		public void testForEachIntegerSet()
		{
			CheckOutput("loops/forEachIntegerSet.o");
		}

		[Test]
		public void testForEachIntegerSetWithIndex()
		{
			CheckOutput("loops/forEachIntegerSetWithIndex.o");
		}

		[Test]
		public void testForEachTimeRange()
		{
			CheckOutput("loops/forEachTimeRange.o");
		}

		[Test]
		public void testForEachTimeRangeWithIndex()
		{
			CheckOutput("loops/forEachTimeRangeWithIndex.o");
		}

		[Test]
		public void testForEachTupleList()
		{
			CheckOutput("loops/forEachTupleList.o");
		}

		[Test]
		public void testForEachTupleListWithIndex()
		{
			CheckOutput("loops/forEachTupleListWithIndex.o");
		}

		[Test]
		public void testForEachTupleSet()
		{
			CheckOutput("loops/forEachTupleSet.o");
		}

		[Test]
		public void testForEachTupleSetWithIndex()
		{
			CheckOutput("loops/forEachTupleSetWithIndex.o");
		}

		[Test]
		public void testWhile()
		{
			CheckOutput("loops/while.o");
		}

	}
}

