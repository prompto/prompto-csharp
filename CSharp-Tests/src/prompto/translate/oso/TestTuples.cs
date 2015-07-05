// generated: 2015-07-05T23:01:01.447
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oso
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

