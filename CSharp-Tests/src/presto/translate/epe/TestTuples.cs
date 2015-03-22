using NUnit.Framework;
using presto.parser;

namespace presto.translate.epe
{

	[TestFixture]
	public class TestTuples : BaseEParserTest
	{

		[Test]
		public void testMultiAssignment()
		{
			compareResourceEPE("tuples/multiAssignment.e");
		}

		[Test]
		public void testSingleAssignment()
		{
			compareResourceEPE("tuples/singleAssignment.e");
		}

		[Test]
		public void testTupleElement()
		{
			compareResourceEPE("tuples/tupleElement.e");
		}

	}
}

