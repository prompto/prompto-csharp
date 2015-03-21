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
			compareResourceEOE("tuples/multiAssignment.e");
		}

		[Test]
		public void testSingleAssignment()
		{
			compareResourceEOE("tuples/singleAssignment.e");
		}

		[Test]
		public void testTupleElement()
		{
			compareResourceEOE("tuples/tupleElement.e");
		}

	}
}

