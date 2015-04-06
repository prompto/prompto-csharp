using NUnit.Framework;
using presto.parser;

namespace presto.translate.ese
{

	[TestFixture]
	public class TestTuples : BaseEParserTest
	{

		[Test]
		public void testMultiAssignment()
		{
			compareResourceESE("tuples/multiAssignment.pec");
		}

		[Test]
		public void testSingleAssignment()
		{
			compareResourceESE("tuples/singleAssignment.pec");
		}

		[Test]
		public void testTupleElement()
		{
			compareResourceESE("tuples/tupleElement.pec");
		}

	}
}

