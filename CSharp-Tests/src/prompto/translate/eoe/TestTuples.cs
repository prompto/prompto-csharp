// generated: 2015-07-05T23:01:01.443
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.eoe
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

