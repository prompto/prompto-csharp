using NUnit.Framework;
using presto.parser;

namespace presto.translate.opo
{

	[TestFixture]
	public class TestTuples : BaseOParserTest
	{

		[Test]
		public void testMultiAssignment()
		{
			compareResourceOPO("tuples/multiAssignment.o");
		}

		[Test]
		public void testSingleAssignment()
		{
			compareResourceOPO("tuples/singleAssignment.o");
		}

		[Test]
		public void testTupleElement()
		{
			compareResourceOPO("tuples/tupleElement.o");
		}

	}
}

