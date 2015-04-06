using NUnit.Framework;
using presto.parser;

namespace presto.translate.oso
{

	[TestFixture]
	public class TestTuples : BaseOParserTest
	{

		[Test]
		public void testMultiAssignment()
		{
			compareResourceOSO("tuples/multiAssignment.poc");
		}

		[Test]
		public void testSingleAssignment()
		{
			compareResourceOSO("tuples/singleAssignment.poc");
		}

		[Test]
		public void testTupleElement()
		{
			compareResourceOSO("tuples/tupleElement.poc");
		}

	}
}

