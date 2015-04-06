using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.o
{

	[TestFixture]
	public class TestTuples : BaseOParserTest
	{

		[SetUp]
		public void before()
		{
			Out.init();
		}

		[TearDown]
		public void after()
		{
			Out.restore();
		}

		[Test]
		public void testMultiAssignment()
		{
			CheckOutput("tuples/multiAssignment.poc");
		}

		[Test]
		public void testSingleAssignment()
		{
			CheckOutput("tuples/singleAssignment.poc");
		}

		[Test]
		public void testTupleElement()
		{
			CheckOutput("tuples/tupleElement.poc");
		}

	}
}

