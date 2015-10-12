using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.ese
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

