using NUnit.Framework;
using presto.parser;
using presto.utils;

namespace presto.runtime.e
{

	[TestFixture]
	public class TestTuples : BaseEParserTest
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
			CheckOutput("tuples/multiAssignment.e");
		}

		[Test]
		public void testSingleAssignment()
		{
			CheckOutput("tuples/singleAssignment.e");
		}

		[Test]
		public void testTupleElement()
		{
			CheckOutput("tuples/tupleElement.e");
		}

	}
}

