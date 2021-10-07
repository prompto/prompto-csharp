using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestStore : BaseEParserTest
	{

		[Test]
		public void testAsyncFetchMany()
		{
			compareResourceEOE("store/asyncFetchMany.pec");
		}

		[Test]
		public void testAsyncFetchOne()
		{
			compareResourceEOE("store/asyncFetchOne.pec");
		}

		[Test]
		public void testAsyncStore()
		{
			compareResourceEOE("store/asyncStore.pec");
		}

		[Test]
		public void testAuditDelete()
		{
			compareResourceEOE("store/auditDelete.pec");
		}

		[Test]
		public void testAuditInsert()
		{
			compareResourceEOE("store/auditInsert.pec");
		}

		[Test]
		public void testAuditMany()
		{
			compareResourceEOE("store/auditMany.pec");
		}

		[Test]
		public void testAuditUpdate()
		{
			compareResourceEOE("store/auditUpdate.pec");
		}

		[Test]
		public void testDeleteRecords()
		{
			compareResourceEOE("store/deleteRecords.pec");
		}

		[Test]
		public void testFetchAnd()
		{
			compareResourceEOE("store/fetchAnd.pec");
		}

		[Test]
		public void testFetchBoolean()
		{
			compareResourceEOE("store/fetchBoolean.pec");
		}

		[Test]
		public void testFetchContains()
		{
			compareResourceEOE("store/fetchContains.pec");
		}

		[Test]
		public void testFetchGreater()
		{
			compareResourceEOE("store/fetchGreater.pec");
		}

		[Test]
		public void testFetchGreaterEqual()
		{
			compareResourceEOE("store/fetchGreaterEqual.pec");
		}

		[Test]
		public void testFetchHas()
		{
			compareResourceEOE("store/fetchHas.pec");
		}

		[Test]
		public void testFetchIn()
		{
			compareResourceEOE("store/fetchIn.pec");
		}

		[Test]
		public void testFetchLesser()
		{
			compareResourceEOE("store/fetchLesser.pec");
		}

		[Test]
		public void testFetchLesserEqual()
		{
			compareResourceEOE("store/fetchLesserEqual.pec");
		}

		[Test]
		public void testFetchNotBoolean()
		{
			compareResourceEOE("store/fetchNotBoolean.pec");
		}

		[Test]
		public void testFetchNotContains()
		{
			compareResourceEOE("store/fetchNotContains.pec");
		}

		[Test]
		public void testFetchNotHas()
		{
			compareResourceEOE("store/fetchNotHas.pec");
		}

		[Test]
		public void testFetchNotIn()
		{
			compareResourceEOE("store/fetchNotIn.pec");
		}

		[Test]
		public void testFetchOr()
		{
			compareResourceEOE("store/fetchOr.pec");
		}

		[Test]
		public void testFlush()
		{
			compareResourceEOE("store/flush.pec");
		}

		[Test]
		public void testListRecords()
		{
			compareResourceEOE("store/listRecords.pec");
		}

		[Test]
		public void testManyRecords()
		{
			compareResourceEOE("store/manyRecords.pec");
		}

		[Test]
		public void testManyUntypedRecords()
		{
			compareResourceEOE("store/manyUntypedRecords.pec");
		}

		[Test]
		public void testSimpleRecord()
		{
			compareResourceEOE("store/simpleRecord.pec");
		}

		[Test]
		public void testSlicedRecords()
		{
			compareResourceEOE("store/slicedRecords.pec");
		}

		[Test]
		public void testSortedRecords()
		{
			compareResourceEOE("store/sortedRecords.pec");
		}

		[Test]
		public void testSubRecord()
		{
			compareResourceEOE("store/subRecord.pec");
		}

		[Test]
		public void testUntypedRecord()
		{
			compareResourceEOE("store/untypedRecord.pec");
		}

	}
}

