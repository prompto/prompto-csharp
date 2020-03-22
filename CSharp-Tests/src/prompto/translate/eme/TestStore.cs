using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eme
{

	[TestFixture]
	public class TestStore : BaseEParserTest
	{

		[Test]
		public void testAsyncFetchMany()
		{
			compareResourceEME("store/asyncFetchMany.pec");
		}

		[Test]
		public void testAsyncFetchOne()
		{
			compareResourceEME("store/asyncFetchOne.pec");
		}

		[Test]
		public void testAsyncStore()
		{
			compareResourceEME("store/asyncStore.pec");
		}

		[Test]
		public void testDeleteRecords()
		{
			compareResourceEME("store/deleteRecords.pec");
		}

		[Test]
		public void testFetchAnd()
		{
			compareResourceEME("store/fetchAnd.pec");
		}

		[Test]
		public void testFetchBoolean()
		{
			compareResourceEME("store/fetchBoolean.pec");
		}

		[Test]
		public void testFetchNotBoolean()
		{
			compareResourceEME("store/fetchNotBoolean.pec");
		}

		[Test]
		public void testFetchOr()
		{
			compareResourceEME("store/fetchOr.pec");
		}

		[Test]
		public void testFlush()
		{
			compareResourceEME("store/flush.pec");
		}

		[Test]
		public void testListRecords()
		{
			compareResourceEME("store/listRecords.pec");
		}

		[Test]
		public void testManyRecords()
		{
			compareResourceEME("store/manyRecords.pec");
		}

		[Test]
		public void testManyUntypedRecords()
		{
			compareResourceEME("store/manyUntypedRecords.pec");
		}

		[Test]
		public void testSimpleRecord()
		{
			compareResourceEME("store/simpleRecord.pec");
		}

		[Test]
		public void testSlicedRecords()
		{
			compareResourceEME("store/slicedRecords.pec");
		}

		[Test]
		public void testSortedRecords()
		{
			compareResourceEME("store/sortedRecords.pec");
		}

		[Test]
		public void testSubRecord()
		{
			compareResourceEME("store/subRecord.pec");
		}

		[Test]
		public void testUntypedRecord()
		{
			compareResourceEME("store/untypedRecord.pec");
		}

	}
}

