using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestLoops : BaseEParserTest
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
			CheckOutput("loops/doWhile.e");
		}

		[Test]
		public void testForEachCharacterRange()
		{
			CheckOutput("loops/forEachCharacterRange.e");
		}

		[Test]
		public void testForEachCharacterRangeWithIndex()
		{
			CheckOutput("loops/forEachCharacterRangeWithIndex.e");
		}

		[Test]
		public void testForEachDateRange()
		{
			CheckOutput("loops/forEachDateRange.e");
		}

		[Test]
		public void testForEachDateRangeWithIndex()
		{
			CheckOutput("loops/forEachDateRangeWithIndex.e");
		}

		[Test]
		public void testForEachDictionaryItem()
		{
			CheckOutput("loops/forEachDictionaryItem.e");
		}

		[Test]
		public void testForEachDictionaryItemWithIndex()
		{
			CheckOutput("loops/forEachDictionaryItemWithIndex.e");
		}

		[Test]
		public void testForEachDictionaryKey()
		{
			CheckOutput("loops/forEachDictionaryKey.e");
		}

		[Test]
		public void testForEachDictionaryKeyWithIndex()
		{
			CheckOutput("loops/forEachDictionaryKeyWithIndex.e");
		}

		[Test]
		public void testForEachDictionaryValue()
		{
			CheckOutput("loops/forEachDictionaryValue.e");
		}

		[Test]
		public void testForEachDictionaryValueWithIndex()
		{
			CheckOutput("loops/forEachDictionaryValueWithIndex.e");
		}

		[Test]
		public void testForEachInstanceList()
		{
			CheckOutput("loops/forEachInstanceList.e");
		}

		[Test]
		public void testForEachInstanceListWithIndex()
		{
			CheckOutput("loops/forEachInstanceListWithIndex.e");
		}

		[Test]
		public void testForEachInstanceSet()
		{
			CheckOutput("loops/forEachInstanceSet.e");
		}

		[Test]
		public void testForEachInstanceSetWithIndex()
		{
			CheckOutput("loops/forEachInstanceSetWithIndex.e");
		}

		[Test]
		public void testForEachIntegerList()
		{
			CheckOutput("loops/forEachIntegerList.e");
		}

		[Test]
		public void testForEachIntegerListWithIndex()
		{
			CheckOutput("loops/forEachIntegerListWithIndex.e");
		}

		[Test]
		public void testForEachIntegerRange()
		{
			CheckOutput("loops/forEachIntegerRange.e");
		}

		[Test]
		public void testForEachIntegerRangeWithIndex()
		{
			CheckOutput("loops/forEachIntegerRangeWithIndex.e");
		}

		[Test]
		public void testForEachIntegerSet()
		{
			CheckOutput("loops/forEachIntegerSet.e");
		}

		[Test]
		public void testForEachIntegerSetWithIndex()
		{
			CheckOutput("loops/forEachIntegerSetWithIndex.e");
		}

		[Test]
		public void testForEachTimeRange()
		{
			CheckOutput("loops/forEachTimeRange.e");
		}

		[Test]
		public void testForEachTimeRangeWithIndex()
		{
			CheckOutput("loops/forEachTimeRangeWithIndex.e");
		}

		[Test]
		public void testForEachTupleList()
		{
			CheckOutput("loops/forEachTupleList.e");
		}

		[Test]
		public void testForEachTupleListWithIndex()
		{
			CheckOutput("loops/forEachTupleListWithIndex.e");
		}

		[Test]
		public void testForEachTupleSet()
		{
			CheckOutput("loops/forEachTupleSet.e");
		}

		[Test]
		public void testForEachTupleSetWithIndex()
		{
			CheckOutput("loops/forEachTupleSetWithIndex.e");
		}

		[Test]
		public void testWhile()
		{
			CheckOutput("loops/while.e");
		}

	}
}

