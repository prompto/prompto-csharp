using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestStore : BaseEParserTest
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
		public void testAsyncFetchMany()
		{
			CheckOutput("store/asyncFetchMany.pec");
		}

		[Test]
		public void testAsyncFetchOne()
		{
			CheckOutput("store/asyncFetchOne.pec");
		}

		[Test]
		public void testAsyncStore()
		{
			CheckOutput("store/asyncStore.pec");
		}

		[Test]
		public void testDeleteRecords()
		{
			CheckOutput("store/deleteRecords.pec");
		}

		[Test]
		public void testFetchAnd()
		{
			CheckOutput("store/fetchAnd.pec");
		}

		[Test]
		public void testFetchBoolean()
		{
			CheckOutput("store/fetchBoolean.pec");
		}

		[Test]
		public void testFetchContains()
		{
			CheckOutput("store/fetchContains.pec");
		}

		[Test]
		public void testFetchGreater()
		{
			CheckOutput("store/fetchGreater.pec");
		}

		[Test]
		public void testFetchGreaterEqual()
		{
			CheckOutput("store/fetchGreaterEqual.pec");
		}

		[Test]
		public void testFetchHas()
		{
			CheckOutput("store/fetchHas.pec");
		}

		[Test]
		public void testFetchIn()
		{
			CheckOutput("store/fetchIn.pec");
		}

		[Test]
		public void testFetchLesser()
		{
			CheckOutput("store/fetchLesser.pec");
		}

		[Test]
		public void testFetchLesserEqual()
		{
			CheckOutput("store/fetchLesserEqual.pec");
		}

		[Test]
		public void testFetchNotBoolean()
		{
			CheckOutput("store/fetchNotBoolean.pec");
		}

		[Test]
		public void testFetchNotContains()
		{
			CheckOutput("store/fetchNotContains.pec");
		}

		[Test]
		public void testFetchNotHas()
		{
			CheckOutput("store/fetchNotHas.pec");
		}

		[Test]
		public void testFetchNotIn()
		{
			CheckOutput("store/fetchNotIn.pec");
		}

		[Test]
		public void testFetchOr()
		{
			CheckOutput("store/fetchOr.pec");
		}

		[Test]
		public void testFlush()
		{
			CheckOutput("store/flush.pec");
		}

		[Test]
		public void testListRecords()
		{
			CheckOutput("store/listRecords.pec");
		}

		[Test]
		public void testManyRecords()
		{
			CheckOutput("store/manyRecords.pec");
		}

		[Test]
		public void testManyUntypedRecords()
		{
			CheckOutput("store/manyUntypedRecords.pec");
		}

		[Test]
		public void testSimpleRecord()
		{
			CheckOutput("store/simpleRecord.pec");
		}

		[Test]
		public void testSlicedRecords()
		{
			CheckOutput("store/slicedRecords.pec");
		}

		[Test]
		public void testSortedRecords()
		{
			CheckOutput("store/sortedRecords.pec");
		}

		[Test]
		public void testSubRecord()
		{
			CheckOutput("store/subRecord.pec");
		}

		[Test]
		public void testUntypedRecord()
		{
			CheckOutput("store/untypedRecord.pec");
		}

	}
}

