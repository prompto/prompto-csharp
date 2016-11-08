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
		public void testDeleteRecords()
		{
			CheckOutput("store/deleteRecords.pec");
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

