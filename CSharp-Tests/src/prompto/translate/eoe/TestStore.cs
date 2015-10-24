using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
{

	[TestFixture]
	public class TestStore : BaseEParserTest
	{

		[Test]
		public void testManyRecords()
		{
			compareResourceEOE("store/manyRecords.pec");
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

	}
}

