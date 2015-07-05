// generated: 2015-07-05T23:01:01.446
using NUnit.Framework;
using prompto.parser;

namespace prompto.translate.oeo
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

