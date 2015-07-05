// generated: 2015-07-05T23:01:01.445
using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
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
			CheckOutput("tuples/multiAssignment.pec");
		}

		[Test]
		public void testSingleAssignment()
		{
			CheckOutput("tuples/singleAssignment.pec");
		}

		[Test]
		public void testTupleElement()
		{
			CheckOutput("tuples/tupleElement.pec");
		}

	}
}

