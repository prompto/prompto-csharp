using NUnit.Framework;
using prompto.parser;
using prompto.utils;

namespace prompto.runtime.e
{

	[TestFixture]
	public class TestExit : BaseEParserTest
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
		public void testAssignedReturn()
		{
			CheckOutput("exit/assignedReturn.pec");
		}

		[Test]
		public void testAssignedReturnInDoWhile()
		{
			CheckOutput("exit/assignedReturnInDoWhile.pec");
		}

		[Test]
		public void testAssignedReturnInForEach()
		{
			CheckOutput("exit/assignedReturnInForEach.pec");
		}

		[Test]
		public void testAssignedReturnInIf()
		{
			CheckOutput("exit/assignedReturnInIf.pec");
		}

		[Test]
		public void testAssignedReturnInWhile()
		{
			CheckOutput("exit/assignedReturnInWhile.pec");
		}

		[Test]
		public void testUnassignedReturn()
		{
			CheckOutput("exit/unassignedReturn.pec");
		}

		[Test]
		public void testUnassignedReturnInDoWhile()
		{
			CheckOutput("exit/unassignedReturnInDoWhile.pec");
		}

		[Test]
		public void testUnassignedReturnInForEach()
		{
			CheckOutput("exit/unassignedReturnInForEach.pec");
		}

		[Test]
		public void testUnassignedReturnInIf()
		{
			CheckOutput("exit/unassignedReturnInIf.pec");
		}

		[Test]
		public void testUnassignedReturnInWhile()
		{
			CheckOutput("exit/unassignedReturnInWhile.pec");
		}

	}
}

