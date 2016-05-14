using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
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
			CheckOutput("loops/doWhile.pec");
		}

		[Test]
		public void testEmbeddedForEach()
		{
			CheckOutput("loops/embeddedForEach.pec");
		}

		[Test]
		public void testForEachCharacterRange()
		{
			CheckOutput("loops/forEachCharacterRange.pec");
		}

		[Test]
		public void testForEachCharacterRangeWithIndex()
		{
			CheckOutput("loops/forEachCharacterRangeWithIndex.pec");
		}

		[Test]
		public void testForEachDateRange()
		{
			CheckOutput("loops/forEachDateRange.pec");
		}

		[Test]
		public void testForEachDateRangeWithIndex()
		{
			CheckOutput("loops/forEachDateRangeWithIndex.pec");
		}

		[Test]
		public void testForEachDictionaryItem()
		{
			CheckOutput("loops/forEachDictionaryItem.pec");
		}

		[Test]
		public void testForEachDictionaryItemWithIndex()
		{
			CheckOutput("loops/forEachDictionaryItemWithIndex.pec");
		}

		[Test]
		public void testForEachDictionaryKey()
		{
			CheckOutput("loops/forEachDictionaryKey.pec");
		}

		[Test]
		public void testForEachDictionaryKeyWithIndex()
		{
			CheckOutput("loops/forEachDictionaryKeyWithIndex.pec");
		}

		[Test]
		public void testForEachDictionaryValue()
		{
			CheckOutput("loops/forEachDictionaryValue.pec");
		}

		[Test]
		public void testForEachDictionaryValueWithIndex()
		{
			CheckOutput("loops/forEachDictionaryValueWithIndex.pec");
		}

		[Test]
		public void testForEachInstanceList()
		{
			CheckOutput("loops/forEachInstanceList.pec");
		}

		[Test]
		public void testForEachInstanceListWithIndex()
		{
			CheckOutput("loops/forEachInstanceListWithIndex.pec");
		}

		[Test]
		public void testForEachInstanceSet()
		{
			CheckOutput("loops/forEachInstanceSet.pec");
		}

		[Test]
		public void testForEachInstanceSetWithIndex()
		{
			CheckOutput("loops/forEachInstanceSetWithIndex.pec");
		}

		[Test]
		public void testForEachIntegerList()
		{
			CheckOutput("loops/forEachIntegerList.pec");
		}

		[Test]
		public void testForEachIntegerListWithIndex()
		{
			CheckOutput("loops/forEachIntegerListWithIndex.pec");
		}

		[Test]
		public void testForEachIntegerRange()
		{
			CheckOutput("loops/forEachIntegerRange.pec");
		}

		[Test]
		public void testForEachIntegerRangeWithIndex()
		{
			CheckOutput("loops/forEachIntegerRangeWithIndex.pec");
		}

		[Test]
		public void testForEachIntegerSet()
		{
			CheckOutput("loops/forEachIntegerSet.pec");
		}

		[Test]
		public void testForEachIntegerSetWithIndex()
		{
			CheckOutput("loops/forEachIntegerSetWithIndex.pec");
		}

		[Test]
		public void testForEachTimeRange()
		{
			CheckOutput("loops/forEachTimeRange.pec");
		}

		[Test]
		public void testForEachTimeRangeWithIndex()
		{
			CheckOutput("loops/forEachTimeRangeWithIndex.pec");
		}

		[Test]
		public void testForEachTupleList()
		{
			CheckOutput("loops/forEachTupleList.pec");
		}

		[Test]
		public void testForEachTupleListWithIndex()
		{
			CheckOutput("loops/forEachTupleListWithIndex.pec");
		}

		[Test]
		public void testForEachTupleSet()
		{
			CheckOutput("loops/forEachTupleSet.pec");
		}

		[Test]
		public void testForEachTupleSetWithIndex()
		{
			CheckOutput("loops/forEachTupleSetWithIndex.pec");
		}

		[Test]
		public void testWhile()
		{
			CheckOutput("loops/while.pec");
		}

	}
}

