using NUnit.Framework;
using presto.parser;

namespace presto.translate.oeo
{

	[TestFixture]
	public class TestTuples : BaseOParserTest
	{

		[Test]
		public void testMultiAssignment()
		{
			compareResourceOEO("tuples/multiAssignment.poc");
		}

		[Test]
		public void testSingleAssignment()
		{
			compareResourceOEO("tuples/singleAssignment.poc");
		}

		[Test]
		public void testTupleElement()
		{
			compareResourceOEO("tuples/tupleElement.poc");
		}

	}
}

