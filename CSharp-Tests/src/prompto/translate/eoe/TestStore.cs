using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestStore : BaseEParserTest
	{

		[Test]
		public void testAsyncFetch()
		{
			compareResourceEOE("store/asyncFetch.pec");
		}

		[Test]
		public void testAsyncStore()
		{
			compareResourceEOE("store/asyncStore.pec");
		}

		[Test]
		public void testDeleteRecords()
		{
			compareResourceEOE("store/deleteRecords.pec");
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

