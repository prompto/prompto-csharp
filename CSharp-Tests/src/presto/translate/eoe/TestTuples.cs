using NUnit.Framework;
using presto.parser;

namespace presto.translate.eoe
{

	[TestFixture]
	public class TestTuples : BaseEParserTest
	{

		[Test]
		public void testMultiAssignment()
		{
			compareResourceEOE("tuples/multiAssignment.pec");
		}

		[Test]
		public void testSingleAssignment()
		{
			compareResourceEOE("tuples/singleAssignment.pec");
		}

		[Test]
		public void testTupleElement()
		{
			compareResourceEOE("tuples/tupleElement.pec");
		}

	}
}

