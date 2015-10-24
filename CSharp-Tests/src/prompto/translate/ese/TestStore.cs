using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
{

	[TestFixture]
	public class TestStore : BaseEParserTest
	{

		[Test]
		public void testManyRecords()
		{
			compareResourceESE("store/manyRecords.pec");
		}

		[Test]
		public void testSimpleRecord()
		{
			compareResourceESE("store/simpleRecord.pec");
		}

		[Test]
		public void testSlicedRecords()
		{
			compareResourceESE("store/slicedRecords.pec");
		}

		[Test]
		public void testSortedRecords()
		{
			compareResourceESE("store/sortedRecords.pec");
		}

		[Test]
		public void testSubRecord()
		{
			compareResourceESE("store/subRecord.pec");
		}

	}
}

